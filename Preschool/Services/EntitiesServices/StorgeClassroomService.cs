using Azure;
using Azure.Data.Tables;
using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public class StorgeClassroomService : IStorgeClassroomService
    {
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        public StorgeClassroomService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobStorge"]);
        }

        public async void AddClassroomToTable(ClassroomEntity classroomEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ClassroomsTable");
            tableClient.CreateIfNotExists();
            classroomEntity.Id = random.Next(0, 10000000);
            classroomEntity.RowKey = classroomEntity.Id.ToString();
            await tableClient.AddEntityAsync(classroomEntity);

        }

        public List<ClassroomEntity> GetClassroomEntities()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ClassroomsTable");
            tableClient.CreateIfNotExists();
            var list = tableClient.Query<ClassroomEntity>().ToList();
            return list;
        }


        public ClassroomEntity GetClassroomEntityById(int? id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ClassroomsTable");
            tableClient.CreateIfNotExists();
            var classroomEntity = tableClient.Query<ClassroomEntity>().FirstOrDefault(c => c.Id == id);
            return classroomEntity;
        }



        public void DeleteClassroomEntity(ClassroomEntity classroomEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ClassroomsTable");
            tableClient.CreateIfNotExists();
            tableClient.DeleteEntity(classroomEntity.PartitionKey, classroomEntity.RowKey);

        }

        public void UpdateClassroomEntity(ClassroomEntity classroomEntitytoUpdate)
        {

            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ClassroomsTable");
            try
            {
                tableClient.CreateIfNotExists();
                var classroomEntity = tableClient.Query<ClassroomEntity>().FirstOrDefault(c => c.Id == classroomEntitytoUpdate.Id);
                classroomEntity.Name = classroomEntitytoUpdate.Name;
                classroomEntity.Icon = classroomEntitytoUpdate.Icon;
                classroomEntity.Color = classroomEntitytoUpdate.Color;  
                
                tableClient.UpdateEntity(classroomEntity, classroomEntity.ETag);
            }
            catch (RequestFailedException ex)
            {
                // Handle exceptions, e.g., concurrency conflicts.
                Console.WriteLine($"Error updating entity: {ex.Message}");
            }
        }


        public bool IsClassroomExists(int id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ClassroomsTable");
            tableClient.CreateIfNotExists();
            return tableClient.Query<ClassroomEntity>().Any(c => c.Id == id);
        }
    }
}
