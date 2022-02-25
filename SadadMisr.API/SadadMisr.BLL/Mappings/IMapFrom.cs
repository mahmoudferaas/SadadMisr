using AutoMapper;

namespace SadadMisr.BLL.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}