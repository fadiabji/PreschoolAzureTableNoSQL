using Azure.Data.Tables;
using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public class StorgeTeacherService : IStorgeTeacherService
    {
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        public StorgeTeacherService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobStorge"]);
        }

        public async void AddTeacherToTable(TeacherEntity teacher)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "TeachersTable");
            tableClient.CreateIfNotExists();
            teacher.Id = random.Next(0, 10000000);
            teacher.RowKey = teacher.Id.ToString();
            await tableClient.AddEntityAsync(teacher);
        }

        public List<TeacherEntity> GetTeacherEntities()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "TeachersTable");
            tableClient.CreateIfNotExists();
            var list = tableClient.Query<TeacherEntity>().ToList();
            return list;
        }

        public TeacherEntity GetTeacherEntityById(int? id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "TeachersTable");
            tableClient.CreateIfNotExists();
            var teacherEntity = tableClient.Query<TeacherEntity>(c => c.RowKey == id.ToString()).FirstOrDefault();
            return teacherEntity;
        }

        public void DeleteTeacherEntity(TeacherEntity teacherEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "TeachersTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<TeacherEntity>(c => c.RowKey == teacherEntity.RowKey).FirstOrDefault();

            tableClient.DeleteEntity(tableEntity.PartitionKey, tableEntity.RowKey);
        }

        public void UpdateTeacherEntity(TeacherEntity teacherEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "TeachersTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<TeacherEntity>(c => c.RowKey == teacherEntity.RowKey).FirstOrDefault();
            tableEntity.DateOfBirth = teacherEntity.DateOfBirth;
            tableEntity.FirstName = teacherEntity.FirstName;
            tableEntity.IsActive = teacherEntity.IsActive;
            tableEntity.LastName = teacherEntity.LastName; 
            tableEntity.ClassroomId = teacherEntity.ClassroomId;   
            tableEntity.ClassroomJson = teacherEntity.ClassroomJson;
            tableEntity.DocumentsImagesJson = teacherEntity.DocumentsImagesJson;    
            tableEntity.RegistedAt = teacherEntity.RegistedAt;
            

            tableClient.UpdateEntity(tableEntity, tableEntity.ETag);
        }

        public bool IsTeacherExists(int id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "TeachersTable");
            return tableClient.Query<TeacherEntity>().Any(c => c.Id == id);
        }
    }
}
