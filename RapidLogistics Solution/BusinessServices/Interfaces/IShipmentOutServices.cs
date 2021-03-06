﻿using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices.Interfaces
{
    public interface IShipmentOutServices
    {
        string CreateOrUpdate(ShipmentOutEntity shipmentOut);
        int CreateOrUpdateByQuery(ShipmentOutEntity shipmentOut);
        int CreateOrUpdate(List<ShipmentOutEntity> shipmentOutList);
        bool IsExist(string shipmentOutId);
        IEnumerable<ShipmentOutEntity> GetByBoxId(int boxId);
        IEnumerable<ShipmentEntity> GetByBoxIdForReport(int boxId);
        IEnumerable<ShipmentEntity> GetByBoxIdToDisplay(int boxId);
        IEnumerable<ShipmentOutEntity> GetByMasterBillId(int masterBillId);
        IEnumerable<ShipmentEntity> GetByMasterBillIdForReport(int masterBillId);
        IEnumerable<MasterAirwayBillEntity> GetAllMasterBillByDate(DateTime value);
        IEnumerable<BoxOutEntity> GetAllBoxByMasterBill(int masterBillId);
        void Delete(string shipmentId);
        int GetTotalByMasterBill(int id);
        IEnumerable<ShipmentOutEntity> GetByDate(DateTime value);
        IEnumerable<ShipmentEntity> GetByDateForReport(DateTime value);
        IEnumerable<ShipmentOutEntity> GetByDateRange(DateTime value1, DateTime value2);
        IEnumerable<ShipmentEntity> GetListNotDeliveryByQuarter(DateTime start, DateTime end);
        ShipmentOutEntity GetByShipmentId(string shipId);
        bool GetStatusCompletion(string shipmentId);
        string GetDeclarationNo(string shipmentId);
        string GetDateOfCompletion(string shipmentId);
      
    }
}
