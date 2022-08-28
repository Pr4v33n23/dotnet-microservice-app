using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Extensions;
using Play.Common.Repositories;

namespace Play.Catalog.Service.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await _itemsRepository.GetAllAsync()).Select(item => item.AsDTO());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);
            return item.AsDTO();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            { 
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedAt = DateTimeOffset.UtcNow
            };
            var actionName = nameof(GetByIdAsync);
            await _itemsRepository.CreateAsync(item);
            return CreatedAtAction(actionName, new { item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _itemsRepository.GetAsync(id);
            if (existingItem is null) return NotFound();

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await _itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);
            if (item is null) return NotFound();
            await _itemsRepository.RemoveAsync(item.Id);
            return NoContent();
        }
    }

