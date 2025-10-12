﻿using INDT.Common.Insurance.Dto.Request;
using Insurance.INDT.Domain;

namespace Insurance.INDT.Application.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<Result> Register(RegisterInsuranceDto resisterInsuranceDto);

        Task<Result> GetByName(string docto);


        Task<Result> GetAll();
    }
}
