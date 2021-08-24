using System;
using System.ComponentModel.Design;
using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnoTech.VideoApplication2021.Domain.Services;
using InnoTech.VideoApplication2021.Infrastructure.DataAccess.Repositories;
using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;

namespace InnoTech.VideoApplication2021.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cheapish DI (Dependency Injection)
            IVideoRepository repo = new VideoRepositoryInMemory();
            IVideoService service = new VideoService(repo);

            var video1 = new Video()
            {
                Title = "God please help me learn this",
                StoryLine = "There was apon a time",
                ReleaseDate = DateTime.Now
            };
            VideoRepositoryInMemory.Add(video1);

            var video2 = new Video()
            {
                Title = "I am doing the best i can!",
                StoryLine = "Youtube videos for the win!",
                ReleaseDate = DateTime.Now
            };

            var menu = new Menu(service);
            menu.Start();


            
        }
    }
}