using AutoMapper;
using TND.Application.Rooms.Create;
using TND.Application.Rooms.GetByRoomClassIdForGuest;
using TND.Application.Rooms.GetForManagement;
using TND.Application.Rooms.Update;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<CreateRoomCommand, Room>();
            CreateMap<UpdateRoomCommand, Room>();

            CreateMap<PaginatedList<Room>, PaginatedList<RoomForGuestResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<PaginatedList<RoomForManagement>, PaginatedList<RoomForManagementResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

            CreateMap<Room, RoomForGuestResponse>();
            CreateMap<RoomForManagement, RoomForManagementResponse>();

        }
    }
}
