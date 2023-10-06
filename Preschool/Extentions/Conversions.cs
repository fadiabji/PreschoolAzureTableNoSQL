using Preschool.Models.Entities;
using Preschool.Models;
using Newtonsoft.Json;
using Preschool.Models.ViewModels;

namespace Preschool.Extentions
{
    public static class Conversions
    {
        public static ChildEntity ToChildEntity(Child childModel)
        {
            return new ChildEntity()
            {
                PartitionKey = childModel.FirstName,
                RowKey = childModel.Id.ToString(),
                Id = childModel.Id,
                FirstName = childModel.FirstName,
                LastName = childModel.LastName,
                FatherName = childModel.FatherName,
                MotherName = childModel.MotherName,
                Nationality = childModel.Nationality,
                FatherTelephone = childModel.FatherTelephone,
                MotherTelephone = childModel.MotherTelephone,
                DateOfBirthUtc = DateTime.SpecifyKind(childModel.DateOfBirth, DateTimeKind.Utc),
                EnrolDateUtc =  DateTime.SpecifyKind(childModel.EnrolDate, DateTimeKind.Utc),
                ClassroomId = childModel.ClassroomId,
                SubscriptionsJson = JsonConvert.SerializeObject(childModel.Subscriptions),
                DocumentsImageJson= JsonConvert.SerializeObject(childModel.DocumentsImage),
                AttendancesJson = JsonConvert.SerializeObject(childModel.Attendances),
                InvoicesJson = JsonConvert.SerializeObject(childModel.Invoices),
            };
        }

        public static Child ToChild(ChildEntity childEntity)
        {
            return new Child
            {
                Id = childEntity.Id,
                FirstName = childEntity.FirstName,
                LastName = childEntity.LastName,
                FatherName = childEntity.FatherName,
                MotherName = childEntity.MotherName,
                Nationality = childEntity.Nationality,
                FatherTelephone = childEntity.FatherTelephone,
                MotherTelephone = childEntity.MotherTelephone,
                DateOfBirth = childEntity.DateOfBirthUtc,
                EnrolDate = childEntity.DateOfBirthUtc,
                ClassroomId = childEntity.ClassroomId,
                Subscriptions = JsonConvert.DeserializeObject<List<Subscription>>(childEntity.SubscriptionsJson),
                DocumentsImage = JsonConvert.DeserializeObject<List<DocumentsCopies>>(childEntity.DocumentsImageJson),
                Attendances = JsonConvert.DeserializeObject<List<Attendance>>(childEntity.AttendancesJson),
                Invoices = JsonConvert.DeserializeObject<List<Invoice>>(childEntity.InvoicesJson),

            };
        }

        public static List<Child> ToChildren(List<ChildEntity> childEntities)
        {
            var result = new List<Child>();
            foreach(ChildEntity childEntity in childEntities)
            {
                result.Add(Conversions.ToChild(childEntity));
            }
            return result;
               
        }

        public static List<ChildEntity> ToChildrenEntities(List<Child> children)
        {
            var result = new List<ChildEntity>();
            foreach (Child child in children)
            {
                result.Add(Conversions.ToChildEntity(child));
            }
            return result;

        }

        public static ChildVM ToChildVM(Child child)
        {
            ChildVM childVm = new ChildVM()
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                DateOfBirth = child.DateOfBirth,
                MotherTelephone = child.MotherTelephone,
                FatherTelephone = child.FatherTelephone,
                Nationality = child.Nationality,
                EnrolDate = child.EnrolDate,
                FatherName = child.FatherName,
                MotherName = child.MotherName,
                ClassroomId = child.ClassroomId,
            };
            return childVm;
        }

        public static Child ToChild(ChildVM childVm)
        {

            Child child = new Child
            {
                Id = childVm.Id,
                FirstName = childVm.FirstName,
                LastName = childVm.LastName,
                MotherName = childVm.MotherName,
                MotherTelephone = childVm.MotherTelephone,
                FatherTelephone = childVm.FatherTelephone,
                Nationality = childVm.Nationality,
                FatherName = childVm.FatherName,
                DateOfBirth = childVm.DateOfBirth,
                EnrolDate = childVm.EnrolDate,
                ClassroomId = childVm.ClassroomId
            };
            return child;
        }


        public static Models.Asset ToAsset(AssetEntity assetEntity)
        {
            return new Models.Asset
            {
                Id = assetEntity.Id,
                Caption = assetEntity.Caption,
                RegisterdAt = assetEntity.RegisterdAtUtc,
                Name = assetEntity.Name,

            };
        }


        public static AssetEntity ToAssetEntity(Models.Asset asset, string partitionKey, string rowKey)
        {
            return new AssetEntity(partitionKey, rowKey)
            {
                Id = asset.Id,
                Name = asset.Name,
                Caption = asset.Caption,
                RegisterdAtUtc = DateTime.SpecifyKind(asset.RegisterdAt, DateTimeKind.Utc)
            };
        }
        

        public static List<Models.Asset> ToAssets(List<AssetEntity> assetEntities)
        {
            var result = new List<Models.Asset>();
            foreach (AssetEntity assetEntity in assetEntities)
            {
                result.Add(Conversions.ToAsset(assetEntity));
            }
            return result;
        }


        public static List<AssetEntity> ToAssetEntities(List<Models.Asset> assets)
        {
            var result = new List<AssetEntity>();
            foreach (Models.Asset asset in assets)
            {
                result.Add(Conversions.ToAssetEntity(asset, asset.Name, ""  ));
            }
            return result;
        }


        public static Classroom ToClassroom(ClassroomEntity classroomEntity) {
            return new Classroom()
            {
                Id= classroomEntity.Id,
                Name = classroomEntity.Name,
                Color = classroomEntity.Color,
                Icon = classroomEntity.Icon,
                Children = JsonConvert.DeserializeObject<List<Child>>(classroomEntity.ChildrenJson),
                Teachers = JsonConvert.DeserializeObject<List<Teacher>>(classroomEntity.TeachersJson),
            };
        }

        public static ClassroomEntity ToClassroomEntity(Classroom classroom)
        {
            return new ClassroomEntity()
            {
                PartitionKey = classroom.Name,
                RowKey = classroom.Id.ToString(),
                Id = classroom.Id,
                Name = classroom.Name,
                Color = classroom.Color,
                Icon = classroom.Icon,
                ChildrenJson =  JsonConvert.SerializeObject(classroom.Children),
                TeachersJson = JsonConvert.SerializeObject(classroom.Teachers),
            };
        }


        public static List<Classroom> ToClassrooms(List<ClassroomEntity> classroomEntities)
        {
            var result = new List<Classroom>();
            foreach (ClassroomEntity classroomEntity in classroomEntities)
            {
                result.Add(Conversions.ToClassroom(classroomEntity));
            }
            return result;

        }

        public static List<ClassroomEntity> ToClassroomrenEntities(List<Classroom> classrooms)
        {
            var result = new List<ClassroomEntity>();
            foreach (Classroom classroom in classrooms)
            {
                result.Add(Conversions.ToClassroomEntity(classroom));
            }
            return result;

        }


    }
}
