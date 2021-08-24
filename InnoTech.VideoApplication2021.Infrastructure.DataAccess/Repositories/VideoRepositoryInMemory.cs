using System.Collections.Generic;
using InnoTech.VideoApplication2021.Domain.IRepositories;
using InnotTech.VideoApplication2021.Core.Models;

namespace InnoTech.VideoApplication2021.Infrastructure.DataAccess.Repositories
{
    public class VideoRepositoryInMemory : IVideoRepository
    {
        private static List<Video> _videosTable = new List<Video>();
        private static int _id = 1;


        Video IVideoRepository.Add(Video video)
        {
            return Add(video);
        }

        public Video ReadById(int id)
        {
            foreach (var video in _videosTable)
            {
                if (video.Id == id)
                {
                    return video;
                }
            }

            return null;
        }
        public static Video Add(Video video)
        {
            video.Id = _id++;
            _videosTable.Add(video);
            return video;
        }

        public List<Video> FindAll()
        {
            return _videosTable;
        }
        
        //remove later when we use UOW
        public Video Update(Video videoUpdate)
        {
            var videoFromDB = this.ReadById(videoUpdate.Id.Value);
            if (videoFromDB != null)
            {
                videoFromDB.Title = videoUpdate.Title;
                videoFromDB.ReleaseDate = videoUpdate.ReleaseDate;
                videoFromDB.StoryLine = videoUpdate.StoryLine;
                return videoFromDB;
            }

            return null;
        }
        
        public Video Delete(int id)
        {
            var videoFound = this.ReadById(id);
            if (videoFound != null)
            {
                _videosTable.Remove(videoFound);
                return videoFound;
            }

            return null;
        }
    }
}