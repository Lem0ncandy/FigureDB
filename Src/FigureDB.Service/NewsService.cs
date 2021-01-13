﻿using AutoMapper;
using FigureDB.IRepository;
using FigureDB.IService;
using FigureDB.Model.DTO;
using FigureDB.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureDB.Service
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> CreateNews(News news)
        {
            await _repository.InsertAsync(news);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<PaginationDTO<News>> GetNews(int index, int size)
        {
            var news = await _repository.Find()
                .Where(_ => true)
                .Skip(index * size)
                .Take(size)
                .OrderByDescending(n => n.CreateTime)
                .ToListAsync();
            int total = await _repository.Find()
                .Where(_ => true)
                .CountAsync();

            return new PaginationDTO<News>()
            {
                Data = news,
                Total = total
            };
        }

        public async Task<List<News>> GetNewsByFigureId()
        {
            throw new NotImplementedException();
        }
    }
}
