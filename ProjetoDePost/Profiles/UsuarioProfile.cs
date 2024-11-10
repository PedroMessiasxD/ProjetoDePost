﻿using AutoMapper;
using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<Usuario, UsuarioLoginDto>();

            CreateMap<Usuario, UsuarioReadDto>();
        }
    }
}
