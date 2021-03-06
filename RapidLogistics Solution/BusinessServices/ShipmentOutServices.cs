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

    public class ShipmentOutServices : IShipmentOutServices
    {
        private readonly UnitOfWork _unitOfWork;

        public ShipmentOutServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string CreateOrUpdate(ShipmentOutEntity shipmentOut)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<ShipmentOutEntity, ShipmentOut>();
                var ship = Mapper.Map<ShipmentOutEntity, ShipmentOut>(shipmentOut);
                var original = _unitOfWork.ShipmentOutRepository.Get(t => t.ShipmentId == ship.ShipmentId);
                if (original != null)
                {
                    ship.ShipmentId = original.ShipmentId;
                    ship.DateCreated = original.DateCreated;
                    _unitOfWork.ShipmentOutRepository.Update(original, ship);
                }
                else
                {
                    _unitOfWork.ShipmentOutRepository.Insert(ship);
                }
                _unitOfWork.SaveWinform();
                scope.Complete();
                return ship.ShipmentId;
            }
        }
        public int CreateOrUpdateByQuery(ShipmentOutEntity shipmentOut)
        {
            return _unitOfWork.ShipmentOutRepository.ExecuteUpdateQuery(string.Format("INSERT[dbo].[ShipmentOut] ([ShipmentId], [BoxIdRef], [BoxIdString], [MasterBillId], [MasterBillIdString], [DateOut], [EmployeeId], [WarehouseId], [IsSyncOms], [DateInt], [Weight]) VALUES(N'{0}', {1}, N'{2}', {3}, N'{4}', CAST(N'{5}' AS DateTime), {6}, {7}, {8},{9}, {10})", shipmentOut.ShipmentId, shipmentOut.BoxIdRef, shipmentOut.BoxIdString, shipmentOut.MasterBillId, shipmentOut.MasterBillIdString, shipmentOut.DateOut, shipmentOut.EmployeeId, shipmentOut.WarehouseId, 0, shipmentOut.DateInt, shipmentOut.Weight));
        }
        public int CreateOrUpdate(List<ShipmentOutEntity> shipmentOutList)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<ShipmentOutEntity, ShipmentOut>();
                var shipmentOutEntityList = Mapper.Map<List<ShipmentOutEntity>, List<ShipmentOut>>(shipmentOutList);
                int numberInsert = 0;
                foreach (ShipmentOut ship in shipmentOutEntityList)
                {
                    var original = _unitOfWork.ShipmentOutRepository.Get(t => t.ShipmentId == ship.ShipmentId);

                    if (original != null)
                    {
                        ship.ShipmentId = original.ShipmentId;
                        ship.DateCreated = original.DateCreated;

                        if (IsEquals(original, ship))
                            continue;

                        _unitOfWork.ShipmentOutRepository.Update(original, ship);
                    }
                    else
                    {
                        _unitOfWork.ShipmentOutRepository.Insert(ship);
                        numberInsert++;
                    }
                }
                _unitOfWork.SaveWinform();
                scope.Complete();
                return numberInsert;
            }
        }
        private static bool IsEquals(ShipmentOut first, ShipmentOut second)
        {
            if (first == null && second == null)
                return true;
            if (first == null || second == null)
                return false;
            if (first.BoxIdRef == second.BoxIdRef && String.Equals(first.BoxIdString, second.BoxIdString)
                && first.EmployeeId == second.EmployeeId && first.IsSyncOms == second.IsSyncOms
                && first.MasterBillId == second.MasterBillId && String.Equals(first.MasterBillIdString, second.MasterBillIdString)
                && first.ShipmentId == second.ShipmentId && first.WarehouseId == second.WarehouseId)
                return true;

            return false;
        }
        public void Delete(string shipmentId)
        {
            _unitOfWork.ShipmentOutRepository.Delete(shipmentId);
            _unitOfWork.SaveWinform();
        }
        public IEnumerable<ShipmentOutEntity> GetByBoxId(int boxId)
        {
            var shipmentList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.BoxIdRef == boxId).OrderByDescending(t => t.DateOut).Distinct();
            if (shipmentList != null && shipmentList.Any())
            {
                Mapper.CreateMap<ShipmentOut, ShipmentOutEntity>();
                var shipmentListModel = Mapper.Map<List<ShipmentOut>, List<ShipmentOutEntity>>(shipmentList.ToList());
                return shipmentListModel;
            }

            return null;
        }
        public IEnumerable<ShipmentEntity> GetByBoxIdForReport(int boxId)
        {
            var shipmentIdList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.BoxIdRef == boxId).OrderByDescending(t => t.DateOut).Select(t => t.ShipmentId).Distinct();
            if (shipmentIdList != null && shipmentIdList.Any())
            {
                var shipmentList = _unitOfWork.ShipmentRepository.GetMany(t => shipmentIdList.Contains(t.ShipmentId));
                if (shipmentList != null && shipmentList.Any())
                {
                    Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                    var shipmentListModel = Mapper.Map<List<ShipmentInfor>, List<ShipmentEntity>>(shipmentList.ToList());
                    return shipmentListModel;
                }
            }
            return null;
        }
        public IEnumerable<ShipmentEntity> GetByBoxIdToDisplay(int boxId)
        {
            var shipmentListId = _unitOfWork.ShipmentOutRepository.GetMany(t => t.BoxIdRef == boxId).OrderByDescending(t => t.DateOut).Select(p => p.ShipmentId).Distinct();
            if (shipmentListId != null && shipmentListId.Any())
            {
                var listShipment = _unitOfWork.ShipmentRepository.GetMany(t => shipmentListId.Contains(t.ShipmentId));
                Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                var shipmentListModel = Mapper.Map<List<ShipmentInfor>, List<ShipmentEntity>>(listShipment.ToList());
                return shipmentListModel;
            }

            return null;
        }

        public IEnumerable<ShipmentOutEntity> GetByMasterBillId(int masterBillId)
        {
            var shipmentList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.MasterBillId == masterBillId).OrderByDescending(t => t.DateOut).Distinct();
            if (shipmentList != null && shipmentList.Any())
            {
                Mapper.CreateMap<ShipmentOut, ShipmentOutEntity>();
                var shipmentListModel = Mapper.Map<List<ShipmentOut>, List<ShipmentOutEntity>>(shipmentList.ToList());
                return shipmentListModel;
            }

            return null;
        }
        public IEnumerable<ShipmentEntity> GetByMasterBillIdForReport(int masterBillId)
        {
            var shipmentIdList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.MasterBillId == masterBillId).OrderByDescending(t => t.DateOut).Select(t => t.ShipmentId).Distinct();
            if (shipmentIdList != null && shipmentIdList.Any())
            {
                var shipmentList = _unitOfWork.ShipmentRepository.GetMany(t => shipmentIdList.Contains(t.ShipmentId));
                if (shipmentList != null && shipmentList.Any())
                {
                    Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                    var shipmentListModel = Mapper.Map<List<ShipmentInfor>, List<ShipmentEntity>>(shipmentList.ToList());
                    return shipmentListModel;
                }
            }
            return null;
        }
        public IEnumerable<ShipmentOutEntity> GetByDate(DateTime value)
        {
            var shipmentList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.DateOut.Value.Date == value.Date).Distinct();
            if (shipmentList != null && shipmentList.Any())
            {
                Mapper.CreateMap<ShipmentOut, ShipmentOutEntity>();
                var shipmentListModel = Mapper.Map<List<ShipmentOut>, List<ShipmentOutEntity>>(shipmentList.ToList());
                return shipmentListModel;
            }
            return null;
        }

        public IEnumerable<ShipmentEntity> GetByDateForReport(DateTime value)
        {
            var shipmentIdList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.DateOut.Value.Date == value.Date).Select(t => t.ShipmentId).Distinct();
            if (shipmentIdList != null && shipmentIdList.Any())
            {
                var shipmentList = _unitOfWork.ShipmentRepository.GetMany(t => shipmentIdList.Contains(t.ShipmentId));
                if (shipmentList != null && shipmentList.Any())
                {
                    Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                    var shipmentListModel = Mapper.Map<List<ShipmentInfor>, List<ShipmentEntity>>(shipmentList.ToList());
                    return shipmentListModel;
                }
            }
            return null;
        }
        public IEnumerable<MasterAirwayBillEntity> GetAllMasterBillByDate(DateTime value)
        {
            var list = _unitOfWork.ShipmentOutRepository.GetMany(t => t.DateOut.Value.Date == value.Date).Select(t => new MasterAirwayBillEntity { Id = (int)t.MasterBillId, MasterAirwayBill = t.MasterBillIdString });
            return list.GroupBy(t => t.MasterAirwayBill).Select(y => y.First());
        }

        public IEnumerable<BoxOutEntity> GetAllBoxByMasterBill(int masterBillId)
        {
            var list = _unitOfWork.ShipmentOutRepository.GetMany(t => t.MasterBillId == masterBillId).Select(t => new BoxOutEntity { Id = (int)t.BoxIdRef, MasterBillId = (int)t.MasterBillId, BoxId = t.BoxIdString });
            return list.GroupBy(t => t.BoxId).Select(y => y.First());
        }

        public IEnumerable<ShipmentOutEntity> GetByDateRange(DateTime start, DateTime end)
        {
            var masterList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.DateOut.Value.Date >= start.Date && t.DateOut.Value.Date <= end.Date);
            if (masterList != null && masterList.Any())
            {
                Mapper.CreateMap<ShipmentOut, ShipmentOutEntity>();
                var boxInforListModel = Mapper.Map<List<ShipmentOut>, List<ShipmentOutEntity>>(masterList.ToList());
                return boxInforListModel;
            }
            return null;
        }
        public IEnumerable<ShipmentEntity> GetListNotDeliveryByQuarter(DateTime start, DateTime end)
        {
            var shipIdList = _unitOfWork.ShipmentOutRepository.GetMany(t => t.DateOut.Value.Date >= start.Date && t.DateOut.Value.Date <= end.Date).Select(t => t.ShipmentId).Distinct();
            if (shipIdList != null && shipIdList.Any())
            {
                var shipmentList = _unitOfWork.ShipmentRepository.GetMany(t => !shipIdList.Contains(t.ShipmentId));
                Mapper.CreateMap<ShipmentInfor, ShipmentEntity>();
                var boxInforListModel = Mapper.Map<List<ShipmentInfor>, List<ShipmentEntity>>(shipmentList.ToList());
                return boxInforListModel;
            }
            return null;
        }
        public int GetTotalByMasterBill(int id)
        {
            return _unitOfWork.ShipmentOutRepository.GetMany(t => t.MasterBillId == id).Count();
        }
        public bool IsExist(string shipmentId)
        {
            using (var scope = new TransactionScope())
            {
                var shipmentOutEntity = _unitOfWork.ShipmentOutRepository.Exists(s => s.ShipmentId == shipmentId);

                scope.Complete();
                return shipmentOutEntity;
            }
        }

        public ShipmentOutEntity GetByShipmentId(string shipId)
        {
            var shipment = _unitOfWork.ShipmentOutRepository.GetByID(shipId);
            if (shipment != null)
            {
                Mapper.CreateMap<ShipmentOut, ShipmentOutEntity>();
                var shipmentModel = Mapper.Map<ShipmentOut, ShipmentOutEntity>(shipment);
                return shipmentModel;
            }
            return null;
        }
        /// <summary>
        /// VAD1FG,VAD2FG,VAD2FC,VAD3FC
        /// VAD1FG: Chấp nhận thông quan luôn
        /// VAD2FG: Vàng/Đỏ chấp nhận thông quan
        /// VAD2FC: Phân luồng vàng
        /// VAD3FC: Phân luồng đỏ
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public bool GetStatusCompletion(string shipmentId)
        {
            string query = "SELECT MSGCODE FROM CPN_OutputMSG where ShipmentID='" + shipmentId + "'";
            var list = _unitOfWork.ShipmentOutRepository.ExecuteSelectQueryFromECUS5VNACCS(query);

            if (list != null && list.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in list.Rows)
                {
                    string resultCode = Convert.ToString(row["MSGCODE"]);
                    if (!string.IsNullOrEmpty(resultCode) && (resultCode == "VAD1FG" || resultCode == "VAD2FG"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }      
        public string GetDeclarationNo(string shipmentId)
        {
            string query = @"SET NUMERIC_ROUNDABORT OFF; SELECT TOP 30
            a.[Msgxml].value('(/Root/ShipmentID)[1]', 'VARCHAR(MAX)') AS 'ShipmentNo',
            a.[Msgxml].value('(/Root/Declaration/DeclarationNo)[1]', 'VARCHAR(MAX)') AS 'SOTK'
            FROM    (SELECT CAST(Msgxml AS XML) AS xmlMsgxml FROM CPN_OutputMSG where ShipmentID='" + shipmentId + @"') s
            CROSS APPLY xmlMsgxml.nodes('/') a ( Msgxml )";
            var list = _unitOfWork.ShipmentRepository.ExecuteSelectQueryFromECUS5VNACCS(query);

            if (list != null && list.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in list.Rows)
                {
                    string result = Convert.ToString(row["SOTK"]);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }
            }

            return null;
        }
        public string GetDateOfCompletion(string shipmentId)
        {
            string query = @"SELECT TOP 30
            a.[Msgxml].value('(/Root/Declaration/DateOfCompletion)[1]', 'VARCHAR(MAX)') AS 'DateOfCompletion',
            a.[Msgxml].value('(/Root/Declaration/TimeCompletion)[1]', 'VARCHAR(MAX)') AS 'TimeCompletion'
            FROM    (SELECT CAST(Msgxml AS XML) AS xmlMsgxml FROM CPN_OutputMSG where ShipmentID='" + shipmentId + @"') s
            CROSS APPLY xmlMsgxml.nodes('/') a ( Msgxml )";
            var list = _unitOfWork.ShipmentRepository.ExecuteSelectQueryFromECUS5VNACCS(query);

            if (list != null && list.Rows.Count > 0)
            {
                foreach (System.Data.DataRow row in list.Rows)
                {
                    string resultDate = Convert.ToString(row["DateOfCompletion"]);

                    if (!string.IsNullOrEmpty(resultDate))
                    {
                        string fromTimeString = "";
                        if (row["TimeCompletion"] != null)
                        {
                            int resultTime = Convert.ToInt32(row["TimeCompletion"]);
                            TimeSpan result = TimeSpan.FromHours(resultTime);
                            fromTimeString = result.ToString("hh':'mm");
                        }
                        return resultDate + " " + fromTimeString;
                    }
                }
            }
            return null;
        }

    }
}
