using AutoMapper;
using DomainLayer.Entities;
using PresentationLayer.Dtos.Bundle;
using PresentationLayer.Dtos.Category;
using PresentationLayer.Dtos.Product;
using PresentationLayer.Dtos.Subcategory;
using PresentationLayer.Dtos.User;

namespace PresentationLayer.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, AddCategoryDto>().ReverseMap();
        
        CreateMap<Category, ReturnedCategoryDto>()
            .ForMember(dest => dest.ParentCategory, opt => opt.MapFrom(src => src.ParentCategory))
            .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));

        CreateMap<Category, ReturnedParentCategoryDto>().ReverseMap();

        CreateMap<SubCategory, ReturnSubCategoryDto>().ReverseMap();
        CreateMap<SubCategory, ReturnedSubCategoryDto>().ReverseMap();
        
        CreateMap<Category, ReturnedCategoryForSubCate>().ReverseMap();


        CreateMap<SubCategory, ReturnedSubCategoryDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new ReturnedCategoryForSubCate { Id = src.Category.Id, Name = src.Category.Name }))
            .ReverseMap();

            
        CreateMap<CatergoryDto, Category>().ReverseMap();
        
        CreateMap<AddSubCategoryDto, SubCategory>().ReverseMap();


        CreateMap<Product, AddProductDto>().ReverseMap();

        CreateMap<Product, ReturnedProductDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));

        
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        
        CreateMap<Bundle, AddBundleDto>().ReverseMap();

        CreateMap<ProductDto, Product>().ReverseMap();
        
        CreateMap<Bundle, ReturnedBundleDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<ReturnedBundleDto, Bundle>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        
        CreateMap<RegisterUser, User>().ReverseMap();
        
        
        CreateMap<User, ReturnedUserDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "*************"));

        
        
        // UpdateBundleDto
        CreateMap<UpdateBundleDto, Bundle>()
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());
    }
    
}