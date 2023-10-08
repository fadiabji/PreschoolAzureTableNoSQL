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
                ClassroomJosn = JsonConvert.SerializeObject(childModel.Classroom),
                SubscriptionsJson = JsonConvert.SerializeObject(childModel.Subscriptions),
                DocumentsImageJson= JsonConvert.SerializeObject(childModel.DocumentsImage),
                AttendancesJson = JsonConvert.SerializeObject(childModel.Attendances,
                                                                new JsonSerializerSettings
                                                                {
                                                                    ReferenceLoopHandling = 
                                                                    ReferenceLoopHandling.Ignore,
                                                                }),
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
                Classroom = JsonConvert.DeserializeObject<Classroom>(childEntity.ClassroomJosn),
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
                ClassroomId = childVm.ClassroomId,
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


        public static TeacherEntity ToTeacherEntity(Teacher teacher)
        {
            return new TeacherEntity()
            {
                PartitionKey = teacher.FirstName,
                RowKey = teacher.Id.ToString(),
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                DateOfBirth = DateTime.SpecifyKind(teacher.DateOfBirth, DateTimeKind.Utc),
                RegistedAt = DateTime.SpecifyKind(teacher.RegistedAt, DateTimeKind.Utc),
                ClassroomId = teacher.ClassroomId,
                ClassroomJson = JsonConvert.SerializeObject(teacher.Classroom),
                DocumentsImagesJson = JsonConvert.SerializeObject(teacher.DocumentsImage),
            };
        }

        public static Teacher ToTeacher(TeacherEntity teacherEntity)
        {
            return new Teacher
            {
                Id = teacherEntity.Id,
                FirstName = teacherEntity.FirstName,
                LastName = teacherEntity.LastName,       
                DateOfBirth = teacherEntity.DateOfBirth,
                RegistedAt = teacherEntity.RegistedAt,
                ClassroomId = teacherEntity.ClassroomId,
                Classroom = JsonConvert.DeserializeObject<Classroom>(teacherEntity.ClassroomJson),
                DocumentsImage = JsonConvert.DeserializeObject<List<DocumentsCopies>>(teacherEntity.DocumentsImagesJson),


            };
        }


        public static TeacherVM ToTeacherVM(Teacher teacher)
        {
            return new TeacherVM()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                DateOfBirth = teacher.DateOfBirth,
                RegistedAt = teacher.RegistedAt,
                IsActive = teacher.IsActive,
                ClassroomId = teacher.ClassroomId,
            };
        }


        public static List<Teacher> ToTeachers(List<TeacherEntity> teacherEntities)
        {
            var result = new List<Teacher>();
            foreach (TeacherEntity teacherEntity in teacherEntities)
            {
                result.Add(Conversions.ToTeacher(teacherEntity));
            }
            return result;

        }

        public static List<TeacherEntity> ToTeacherrenEntities(List<Teacher> teachers)
        {
            var result = new List<TeacherEntity>();
            foreach (Teacher teacher in teachers)
            {
                result.Add(Conversions.ToTeacherEntity(teacher));
            }
            return result;

        }


        public static AttendanceEntity ToAttendanceEntity(Attendance attendances)
        {
            return new AttendanceEntity()
            {
                PartitionKey = attendances.Status.ToString(),
                RowKey = attendances.Id.ToString(),
                Id = attendances.Id,
                Date = DateTime.SpecifyKind(attendances.Date, DateTimeKind.Utc),
                Status = attendances.Status,
                ChildId = attendances.ChildId,
                ChildJson = JsonConvert.SerializeObject(attendances.Child,
                                                        new JsonSerializerSettings
                                                            {
                                                                ReferenceLoopHandling = 
                                                                ReferenceLoopHandling.Ignore,
                                                            }),
            };
        }

        public static Attendance ToAttendance(AttendanceEntity attendancesEntity)
        {
            return new Attendance()
            {
                Id = attendancesEntity.Id,
                Status = attendancesEntity.Status,
                ChildId = attendancesEntity.ChildId,
                Child = JsonConvert.DeserializeObject<Child>(attendancesEntity.ChildJson),
            };
        }
    }
}
