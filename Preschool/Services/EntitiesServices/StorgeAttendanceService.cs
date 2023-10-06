using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public class StorgeAttendanceService : IStorgeAttendanceService
    {

        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        public StorgeAttendanceService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobStorge"]);
        }

        public async void AddAttendanceToTable(AttendanceEntity attendanceEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AttendancesTable");
            tableClient.CreateIfNotExists();
            attendanceEntity.Id = random.Next(0, 10000000);
            attendanceEntity.RowKey = attendanceEntity.Id.ToString();
            await tableClient.AddEntityAsync(attendanceEntity);

        }

        public List<AttendanceEntity> GetAttendanceEntities()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AttendancesTable");
            tableClient.CreateIfNotExists();
            var list = tableClient.Query<AttendanceEntity>().ToList();
            return list;
        }


        public AttendanceEntity GetAttendanceEntityById(int? id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AttendancesTable");
            tableClient.CreateIfNotExists();
            var attendanceEntity = tableClient.Query<AttendanceEntity>().FirstOrDefault(c => c.Id == id);
            return attendanceEntity;
        }



        public void DeleteAttendanceEntity(AttendanceEntity attendanceEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AttendancesTable");
            tableClient.CreateIfNotExists();
            tableClient.DeleteEntity(attendanceEntity.PartitionKey, attendanceEntity.RowKey);

        }

        public void UpdateAttendanceEntity(AttendanceEntity attendanceEntitytoUpdate)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AttendancesTable");
            tableClient.CreateIfNotExists();
            var attendanceEntity = tableClient.Query<AttendanceEntity>(a => a.RowKey == attendanceEntitytoUpdate.RowKey).FirstOrDefault();
            attendanceEntitytoUpdate.ETag = attendanceEntity.ETag;
            tableClient.UpdateEntity(attendanceEntitytoUpdate, attendanceEntitytoUpdate.ETag);
        }


        public bool IsAttendanceExists(int id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AttendancesTable");
            tableClient.CreateIfNotExists();
            return tableClient.Query<AttendanceEntity>().Any(c => c.Id == id);
        }

    }
}
