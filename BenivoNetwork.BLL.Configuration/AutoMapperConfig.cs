using AutoMapper;
using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL;

namespace BenivoNetwork.BLL.Configuration
{
    public static class AutoMapperConfig
    {
        private static Mapper _mapper;

        public static Mapper Instance
        {
            get
            {
                if (_mapper == null)
                {
                    Register();
                }

                return _mapper;
            }
        }

        public static void Register()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<User, AccountModel>();
                cfg.CreateMap<Message, MessageModel>();
                cfg.CreateMap<Post, PostModel>();
            });

            _mapper = new Mapper(config);
        }
    }
}
