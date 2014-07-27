using Autofac;
using AutoMapper;
using UmbracoDiff.Entities;
using UmbracoDiff.Models;
using UmbracoDiff.ViewModels.CompareTabs;
using UmbracoDiff.ViewModels.Settings;

namespace UmbracoDiff
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterMappings(IContainer container)
        {
            Mapper.Configuration.ConstructServicesUsing(container.Resolve);

            Mapper.CreateMap<UmbracoConnectionModel, UmbracoConnectionViewModel>().ConstructUsingServiceLocator()
                .ForMember(x => x.Header, s => s.MapFrom(src => src.Name));
            Mapper.CreateMap<UmbracoConnectionViewModel, UmbracoConnectionModel>();

            Mapper.CreateMap<CmsNode, CmsNodeViewModel>();
        }
    }
}
