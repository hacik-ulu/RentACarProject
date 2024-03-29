using RentACarProject.Domain.Entities;

namespace RentACarProject.Application.Interfaces.TagCloudInterfaces
{
    public interface ITagCloudRepository
    {
        Task<List<TagCloud>> GetTagCloudsByBlogID(int id);
    }
}
