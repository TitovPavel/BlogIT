using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using BlogIT.DB.BL;
using BlogIT.DB.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using BlogIT.MVC.ViewModels;
using System.Collections.Generic;

namespace BlogIT.MVC.SignalR
{
    public class ChatHub : Hub
    {
        private readonly ILikeService _likeService;
        private readonly INewsService _newsService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        
        public ChatHub(ILikeService likeService,
            INewsService newsService,
            IMapper mapper,
            UserManager<User> userManager,
            IPhotoService photoService)
        {
            _likeService = likeService;
            _newsService = newsService;
            _photoService = photoService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task SendMessage(string message, int newsId)
        {
            string userName = Context.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(userName);
            user.Avatar = _photoService.GetFileByID(user.AvatarId??0);

            ChatMessage chatMessage = new ChatMessage()
            {
                Date = DateTime.Now,
                Message = message,
                User = user,
                NewsId = newsId
            };

            _newsService.AddMessageChat(chatMessage);

            ChatMessageViewModel chatMessageViewModel = _mapper.Map<ChatMessageViewModel>(chatMessage);

            await Clients.Group(newsId.ToString()).SendAsync("ReceiveMessage", chatMessageViewModel);
        }

        public async Task SendLike(int chatMessageId, bool turnOn, bool up)
        {
            string userName = Context.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(userName);

            if (user != null) 
            { 
                _likeService.SendLike(user, chatMessageId, turnOn, up);
            }

        }

        public async Task ConnectUser(int newsId)
        {         
            await Groups.AddToGroupAsync(Context.ConnectionId, newsId.ToString());
         
            List<ChatMessage> chatMessages = _newsService.GetChatMessagesByPartyId(newsId).ToList(); ;

            string userName = Context.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(userName);

            foreach (ChatMessage message in chatMessages)
            {
                ChatMessageViewModel chatMessageViewModel = _mapper.Map<ChatMessageViewModel>(message);

                if (user != null)
                {
                    chatMessageViewModel.LikeCount = _likeService.GetLikeCount(user, chatMessageViewModel.Id);
                }


                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", chatMessageViewModel);
            }         

        }

    }
}
