using System;
using System.Xml.Serialization;
using InnoTech.VideoApplication2021.Infrastructure.DataAccess.Repositories;
using InnotTech.VideoApplication2021.Core.IServices;
using InnotTech.VideoApplication2021.Core.Models;

namespace InnoTech.VideoApplication2021.UI
{
    internal class Menu
    {
        private IVideoService _service;
        public Menu(IVideoService service)
        {
            _service = service;
        }
        public void Start()
        {
            ShowWelcomeGreeting();
            StartLoop();
        }

        private void StartLoop()
        {
            int choice;
            while ((choice = GetMainMenuSelection()) != 0)
            {
                if (choice == 1)
                {
                    CreateVideo();
                } else if (choice == 2)
                {
                    ReadAll();
                } else if (choice == 3)
                {
                    ReadAll();
                    Print(StringConstants.DeletePromptMessage);
                    DeleteVideo(GetVideoSearchMenuSelection());
                } else if (choice == 4)
                {
                    // Update
                    ReadAll();
                    Print(StringConstants.UpdateMoviePrompt);
                    UpdateVideo(GetVideoSearchMenuSelection());
                }
                else if (choice == 5)
                {
                    SearchVideo();
                }
                else if (choice == -1 )
                {
                    PleaseTryAgain();
                }
            }
        }

        private void UpdateVideo(int id)
        {
            
            Console.WriteLine("Enter new title:");
            var newTitle = Console.ReadLine();
            Console.WriteLine("Enter new StoryLine:");
            var newStoryLine = Console.ReadLine();
            var video = _service.ReadById(id);
            video.Title = newTitle;
            video.StoryLine = newStoryLine;
            _service.UpdateVideo(video);
            Console.WriteLine($"Video info updated.");
            
            
        }
        
        private void ReadAll()
        {
            Print("Here are all your videos");
            var videos = _service.ReadAll();
            foreach (var video in videos)
            {
                Print($"{video.Id},{video.Title}, {video.StoryLine}");
            }
        }

        private void Clear()
        {
            Console.Clear();
        }
        private void SearchVideo()
        {
            Print(StringConstants.WhatToSearchFor);
            int choice;
            while ((choice = GetVideoSearchMenuSelection()) != 0)
            {
                if (choice == 1)
                {
                    Print("Type Id to search for");
                    var idToSearchFor = Console.ReadLine();
                    Print($"You searched for Id {idToSearchFor}");
                }
                else if (choice == -1)
                {
                    Print(StringConstants.PleaseSelectCorrectSearchOptions);
                }
            }
        }

        private int GetVideoSearchMenuSelection()
        {
            var selectionString = Console.ReadLine();
            int selection;
            if (int.TryParse(selectionString, out selection))
            {
                return selection;
            }
            return -1;
        }

        private void CreateVideo()
        {
            PrintNewLine();
            Print(StringConstants.CreateVideoGreeting);
            Print(StringConstants.VideoName);
            var videoName = Console.ReadLine();
            Print(StringConstants.VideoStoryLine);
            var videoStoryLine = Console.ReadLine();
            //Call Service
            var video = new Video
            {
                Title = videoName,
                StoryLine = videoStoryLine
            };
            video = _service.Create(video);
            Print($"Video With Following Properties Created - Id: {video.Id.Value} Title: {video.Title} StoryLine: {video.StoryLine}");
            PrintNewLine();
        }

        private Video FindVideoById()
        {
            Console.WriteLine("Insert Video Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number");
            }

            return _service.ReadById(id);
        }

        private void DeleteVideo(int id)
        {
            var video = _service.Delete(id);
            Console.WriteLine($"{video.Title} Has been deleted");
    }

        private void PleaseTryAgain()
        {
            Print(StringConstants.PleaseSelectCorrectItem);
        }

        private int GetMainMenuSelection()
        {
            ShowMainMenu();
            PrintNewLine();
            var selectionString = Console.ReadLine();
            int selection;
            if (int.TryParse(selectionString, out selection))
            {
                return selection;
            }
            return -1;
        }
        
        private void ShowMainMenu()
        {
            PrintNewLine();
            Print(StringConstants.PleaseSelectMain);
            Print(StringConstants.CreateVideoMenuText);
            Print(StringConstants.ShowAllVideosMenuText);
            Print(StringConstants.ExitMainMenuText);
        }

        private void PrintNewLine()
        {
           Console.WriteLine("");
        }

        private void Print(string value)
        {
            Console.WriteLine(value);
        }

        private void ShowWelcomeGreeting()
        {
            Console.WriteLine(StringConstants.WelcomeGreeting);
        }
    }
}