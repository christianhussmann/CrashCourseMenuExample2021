using System.Collections.Generic;
using InnotTech.VideoApplication2021.Core.Models;

namespace InnoTech.VideoApplication2021.Domain.IRepositories
{
    public interface IVideoRepository
    {
        //Add Video
        Video Add(Video video);

        //Read Video
        Video ReadById(int id);

        //Edit Video
        Video UpdateVideo(Video video);

        //Delete videos
        Video Delete(int id);
        
        List<Video> FindAll();
        
    }
}