﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using DataModel.UnitOfWork;
using DataModel;
using System.Transactions;
using AutoMapper;
using BusinessServices.Interfaces;

namespace BusinessServices
{
    public class ShipmentServices : IShipmentServices
    {
        private readonly UnitOfWork _unitOfWork;

        public ShipmentServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int Create(List<ShipmentEntity> shipmentList)
        {
            using (var scope = new TransactionScope())
            {
                if (shipmentList.Any())
                {
                    Mapper.CreateMap<ShipmentEntity, ShipmentInfor>();
                    var shipmentListModel = Mapper.Map<List<ShipmentEntity>, List< ShipmentInfor> >(shipmentList);
                    int count = _unitOfWork.ShipmentRepository.Insert(shipmentListModel);
                    _unitOfWork.SaveWinform();
                    scope.Complete();

                    return count;
                }
            }

            return 0;
        }

        public int Create(ShipmentEntity shipmentEntity)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<ShipmentEntity, ShipmentInfor>();
                var shipmentDataModel = Mapper.Map<ShipmentEntity, ShipmentInfor>(shipmentEntity);
                _unitOfWork.ShipmentRepository.Insert(shipmentDataModel);
                _unitOfWork.SaveWinform();
                scope.Complete();
                return shipmentDataModel.Id;
            }
        }

        public IEnumerable<ShipmentEntity> GetByBoxId(int boxId)
        {
            var shipmentList = _unitOfWork.ShipmentRepository.GetMany(t => t.BoxId == boxId);
            if (shipmentList != null && shipmentList.Any())
            {
                Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                var shipmentListModel = Mapper.Map<List<ShipmentInfor>, List<ShipmentEntity>>(shipmentList.ToList());
                return shipmentListModel;
            }
            return null;
        }

        public IEnumerable<ShipmentEntity> GetByDate(string shipmentId)
        {
            throw new NotImplementedException();
        }

        public ShipmentEntity GetByShipmentId(string shipmentId)
        {
            using (var scope = new TransactionScope())
            {
                ShipmentInfor shipmentDataModel = _unitOfWork.ShipmentRepository.Get(t=> t.ShipmentId.Equals(shipmentId,StringComparison.CurrentCultureIgnoreCase));
                if (shipmentDataModel == null)
                {
                    scope.Complete();
                    return null;
                }
                Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                var shipmentData = Mapper.Map<ShipmentInfor, ShipmentEntity>(shipmentDataModel);
                scope.Complete();
                return shipmentData;
            }
        }

        public bool Exists(string shipmentId)
        {
            return _unitOfWork.ShipmentRepository.Get(t => t.ShipmentId == shipmentId) == null ? false : true;
        }
        public void Delete(int shipmentId)
        {
            _unitOfWork.ShipmentRepository.Delete(shipmentId);
            _unitOfWork.SaveWinform();
        }

        public ShipmentEntity GetByShipmentIdAndBoxId(string shipmentId, int boxId)
        {
            using (var scope = new TransactionScope())
            {
                ShipmentInfor shipmentDataModel = _unitOfWork.ShipmentRepository.Get(t => t.ShipmentId.Equals(shipmentId, StringComparison.CurrentCultureIgnoreCase) && t.BoxId==boxId);
                if (shipmentDataModel == null)
                {
                    scope.Complete();
                    return null;
                }
                Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                var shipmentData = Mapper.Map<ShipmentInfor, ShipmentEntity>(shipmentDataModel);
                scope.Complete();
                return shipmentData;
            }
        }

        public ReportDetailEntity SearchByShipmentId(string shipmentId)
        {
            ReportDetailEntity reportEntity = new ReportDetailEntity();
            var result = _unitOfWork.ShipmentRepository.Get(t => t.ShipmentId.Equals(shipmentId, StringComparison.CurrentCultureIgnoreCase));
            if (result != null)
            {   
                reportEntity.ShipmentId = result.ShipmentId;

                if (result.BoxInfo != null)
                {
                    reportEntity.BoxId = result.BoxInfo.BoxId;
                }
                else
                {
                    reportEntity.BoxId = string.Empty;
                }

                return reportEntity;
            }
            return null;
        }

        public void Delete(string shipmentId)
        {
            _unitOfWork.ShipmentRepository.Delete(t=>t.ShipmentId==shipmentId);
            _unitOfWork.SaveWinform();
        }
    }
}
