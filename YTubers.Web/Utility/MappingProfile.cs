using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Models;
using YTubers.Web.Models.ViewModels;

namespace YTubers.Web.Utility
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<YuTuber, YuTuberViewModel>().ReverseMap();
            CreateMap<YuTuber, YuTuberViewModel2>()
                .ForMember(c => c.YuTuberUserId, s => s.MapFrom(r => r.AppUserId)).ReverseMap();
            CreateMap<ReachRequest, ReachRequestViewModel>().ReverseMap();
            CreateMap<Message, MessageViewModel>().ReverseMap();
        }
    }
}
