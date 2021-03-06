﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        
        [SecuredOperation("brand.add,moderator,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand entity)
        {
            _brandDal.Add(entity);

            return new SuccessResult(Messages.BrandAdded);
        }

        
        [SecuredOperation("brand.delete,moderator,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Delete(Brand entity)
        {
            _brandDal.Delete(entity);

            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
        }

        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == id),Messages.BrandListed);
        }

        
        [SecuredOperation("brand.update,moderator,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult(Messages.BrandUpdated);
        }
    }
}
