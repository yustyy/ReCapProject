﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.add,moderator,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car entity)
        {
            _carDal.Add(entity);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("car.delete,moderator,admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        //[SecuredOperation("car.list,admin")]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), Messages.CarsListed);
        }

        [CacheAspect]
        //[PerformanceAspect(5)]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.CarListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByDailyPrice(int dailyPrice)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice == dailyPrice), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByModelYear(int modelYear)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ModelYear == modelYear), Messages.CarsListed);
        }

        [SecuredOperation("car.update,moderator,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult(Messages.CarUpdated);
        }

        [CacheAspect]
        public IDataResult<List<CarInfoDto>> GetCarsInfo()
        {
            return new SuccessDataResult<List<CarInfoDto>>(_carDal.GetCarDetails(), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<CarImagesDto>> GetAllImagesById(int id)
        {
            return new SuccessDataResult<List<CarImagesDto>>(_carDal.GetCarImageDetails(c=>c.Id==id));
        }

        //[TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            return null;
        }

        [CacheAspect]
        public IDataResult<List<CarInfoDto>> GetCarsInfoByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarInfoDto>>(_carDal.GetCarsInfoByBrandId(brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarInfoDto>> GetCarsInfoByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarInfoDto>>(_carDal.GetCarsInfoByColorId(colorId));
        }
    }
}
