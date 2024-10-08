﻿using Cars.Data.Entities;
using Cars.Data.Interfaces;

namespace Cars.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        public async Task<Car> Insert(Car car)
        {
            var newId = await _carRepository.UpsertAsync(car);
            if (newId > 0)
            {
                car.Id = newId;
            }
            else
            {
                throw new Exception("Failed to insert car");
            }
            return car;
        }

        public async Task<Car> Update(Car car)
        {
            if (car.Id == 0)
            {
                throw new Exception("Id must be set");
            }

            var oldId = car.Id;

            var newId = await _carRepository.UpsertAsync(car);

            if (newId != oldId)
            {
                throw new Exception("Failed to update car");
            }
            return car;
        }

        public async Task Delete(int id)
        {
            var car = await _carRepository.Get(id);

            if (car == null)
            {
                throw new Exception("Car not found");
            }

            await _carRepository.DeleteAsync(id);

            return;
        }
    }
}
