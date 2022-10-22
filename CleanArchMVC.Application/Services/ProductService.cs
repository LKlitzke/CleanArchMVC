using AutoMapper;
using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Interfaces;
using CleanArchMVC.Application.Products.Commands;
using CleanArchMVC.Application.Products.Queries;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            //var productEntity = await _productRepository.GetProductsAsync();
            //return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);

            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
                throw new Exception($"Entity could not be loaded");

            var result = await _mediator.Send(productsQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            //var productEntity = await _productRepository.GetByIdAsync(id);
            //return _mapper.Map<ProductDTO>(productEntity);

            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not loaded.");

            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }

        /*public async Task<ProductDTO> GetProductCategory(int? id)
        {
            //var productEntity = await _productRepository.GetProductCategoryAsync(id);
            //return _mapper.Map<ProductDTO>(productEntity);

            var productByIdQuery = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new Exception($"Entity could not loaded.");

            var result = await _mediator.Send(productByIdQuery);

            return _mapper.Map<ProductDTO>(result);
        }*/

        public async Task Add(ProductDTO productDTO)
        {
            //var productEntity = _mapper.Map<Product>(productDTO);
            //await _productRepository.CreateAsync(productEntity);

            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            //var productEntity = _mapper.Map<Product>(productDTO);
            //await _productRepository.UpdateAsync(productEntity);

            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task Remove(int? id)
        {
            //var productEntity = _productRepository.GetByIdAsync(id).Result;
            //await _productRepository.RemoveAsync(productEntity);

            var productRemoveCommand = _mapper.Map<ProductRemoveCommand>(id.Value);

            if (productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded.");
            await _mediator.Send(productRemoveCommand);
        }
    }
}
