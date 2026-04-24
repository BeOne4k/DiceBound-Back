namespace DiceBound.Mapping;
using AutoMapper;
using DiceBound.DTOs.Character;
using DiceBound.DTOs.Enemy;
using DiceBound.DTOs.Item;
using DiceBound.DTOs.Login;
using DiceBound.DTOs.Mission;
using DiceBound.DTOs.Race;
using DiceBound.DTOs.User;
using DiceBound.Entities.Enums;
using DiceBound.Entity_s.Characters;
using DiceBound.Entity_s.Gameplay;
using DiceBound.Entity_s.Identity;
using DiceBound.Entity_s.Items;

public class DiceBoundProfile : Profile
{
    public DiceBoundProfile()
    {
        // Race
        CreateMap<Race, RaceDto>();
        CreateMap<CreateRaceDto, Race>();

        // Character
        CreateMap<Character, CharacterDto>()
            .ForMember(dest => dest.RaceName,
                       opt => opt.MapFrom(src => src.Race.Name));

        CreateMap<CreateCharacterDto, Character>();

        // Item
        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.Type,
                       opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Rarity,
                       opt => opt.MapFrom(src => src.Rarity.ToString()));

        CreateMap<CreateItemDto, Item>();

        // User
        CreateMap<User, UserDto>();

        CreateMap<UpdateRaceDto, Race>();

        CreateMap<UpdateItemDto, Item>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => Enum.Parse<ItemType>(src.Type)))
            .ForMember(dest => dest.Rarity,
                opt => opt.MapFrom(src => Enum.Parse<ItemRarity>(src.Rarity)));

        //Boss

        CreateMap<Boss, BossDto>();
        CreateMap<CreateBossDto, Boss>();
        CreateMap<UpdateBossDto, Boss>();

        //Mission

        CreateMap<Mission, MissionDto>();
        CreateMap<CreateMissionDto, Mission>();
        CreateMap<UpdateMissionDto, Mission>();

        //Enemy

        CreateMap<Enemy, EnemyDto>();
        CreateMap<CreateEnemyDto, Enemy>();
        CreateMap<UpdateEnemyDto, Enemy>();

        //Auth

        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Username,
                opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore());


        CreateMap<User, string>()
            .ConvertUsing(u => u.Username);
    }
}
