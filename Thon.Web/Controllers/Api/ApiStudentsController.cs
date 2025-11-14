using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Web.Authorization;
using Thon.Web.Entities.Api;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Api;

[ApiController]
[Authorize(Policy = AuthPolicies.Api)]
[Route("students")]
public class ApiStudentsController(
    StudentService studentService, 
    InstitutionService institutionService,
    FacultyService facultyService,
    GroupService groupService) 
    : ControllerBase
{
    [HttpGet("{studentId}")]
    public async Task<ApiStudent> Get(Guid studentId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student with that ID not found!");

        var response = new ApiStudent(student);
        return response;
    }

    [HttpGet("vk/{vkId}")]
    public async Task<ApiStudentSearch> GetByVk(long vkId)
    {
        ThonApiBadRequestException.ThrowIfNegativeOrZero(vkId, "VK ID can't be negative or zero!");

        var searchResult = await studentService.GetByVk(vkId);
        ThonApiNotFoundException.ThrowIfNull(searchResult, "Student with that VK ID not found!");

        return new ApiStudentSearch(searchResult);
    }

    [HttpGet("max/{maxId}")]
    public async Task<ApiStudentSearch> GetByMax(long maxId)
    {
        ThonApiBadRequestException.ThrowIfNegativeOrZero(maxId, "MAX ID can't be negative or zero!");

        var searchResult = await studentService.GetByMax(maxId);
        ThonApiNotFoundException.ThrowIfNull(searchResult, "Student with that MAX ID not found!");

        return new ApiStudentSearch(searchResult);
    }

    [HttpGet("telegram/{telegramId}")]
    public async Task<ApiStudentSearch> GetByTelegram(long telegramId)
    {
        ThonApiBadRequestException.ThrowIfNegativeOrZero(telegramId, "Telegram ID can't be negative or zero!");

        var searchResult = await studentService.GetByTelegram(telegramId);
        ThonApiNotFoundException.ThrowIfNull(searchResult, "Student with that Telegram ID not found!");

        return new ApiStudentSearch(searchResult);
    }

    [HttpPost()]
    public async Task<ApiStudent> Create([FromBody] ApiStudentPost request)
    {
        ThonApiBadRequestException.ThrowIfNull(request, "Request can't be null!");

        if (request.VkId is not null)
        {
            ThonApiBadRequestException.ThrowIfNegativeOrZero(request.VkId.Value, "VK ID can't be negative or zero!");

            var conflictStudent = await studentService.GetByVk(request.VkId.Value);

            if (conflictStudent is not null) 
                throw new ThonApiConflictException("Student with that VK ID already exists!");
        }

        if (request.MaxId is not null)
        {
            ThonApiBadRequestException.ThrowIfNegativeOrZero(request.MaxId.Value, "MAX ID can't be negative or zero!");

            var conflictStudent = await studentService.GetByMax(request.MaxId.Value);

            if (conflictStudent is not null)
                throw new ThonApiConflictException("Student with that MAX ID already exists!");
        }

        if (request.TelegramId is not null)
        {
            ThonApiBadRequestException.ThrowIfNegativeOrZero(request.TelegramId.Value, "Telegram ID can't be negative or zero!");

            var conflictStudent = await studentService.GetByTelegram(request.TelegramId.Value);

            if (conflictStudent is not null)
                throw new ThonApiConflictException("Student with that Telegram ID already exists!");
        }

        if (request.FullName is not null)
            ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.FullName, "Full Name can be NULL but can't be empty!");

        var student = await studentService.Create(
            vkId: request.VkId,
            maxId: request.MaxId,
            telegramId: request.TelegramId,
            fullName: request.FullName);

        return new ApiStudent(student);
    }

    [HttpPatch("{studentId}")]
    public async Task<ApiStudent> Update(Guid studentId, [FromBody] ApiStudentPatch request)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var studentVkId = student.VkId; 
        var studentMaxId = student.MaxId;
        var studentTelegramId = student.TelegramId;
        var studentFullName = student.FullName;

        if (request.VkId is not null)
        {
            ThonApiBadRequestException.ThrowIfNegative(request.VkId.Value, "VK ID can't be negative!");

            if (request.VkId.Value != 0)
            {
                var conflictStudent = await studentService.GetByVk(request.VkId.Value);

                if (conflictStudent is not null && conflictStudent.Id != student.Id)
                    throw new ThonApiConflictException("Student with that VK ID already exists!");

                studentVkId = request.VkId;
            }
            else
                studentVkId = null;
        }

        if (request.MaxId is not null)
        {
            ThonApiBadRequestException.ThrowIfNegative(request.MaxId.Value, "MAX ID can't be negative!");

            if (request.MaxId.Value != 0)
            {
                var conflictStudent = await studentService.GetByMax(request.MaxId.Value);

                if (conflictStudent is not null && conflictStudent.Id != student.Id)
                    throw new ThonApiConflictException("Student with that MAX ID already exists!");

                studentMaxId = request.MaxId;
            }
            else
                studentMaxId = null;
        }

        if (request.TelegramId is not null)
        {
            ThonApiBadRequestException.ThrowIfNegative(request.TelegramId.Value, "Telegram ID can't be negative!");

            if (request.TelegramId.Value != 0)
            {
                var conflictStudent = await studentService.GetByTelegram(request.TelegramId.Value);

                if (conflictStudent is not null && conflictStudent.Id != student.Id)
                    throw new ThonApiConflictException("Student with that Telegram ID already exists!");

                studentTelegramId = request.TelegramId;
            }
            else
                studentTelegramId = null;
        }

        if (request.FullName is not null)
            studentFullName = request.FullName.Trim().Length == 0 ? null : request.FullName;

        student = await studentService.Update(
            student: student,
            vkId: studentVkId,
            maxId: studentMaxId,
            telegramId: studentTelegramId,
            fullName: studentFullName);

        return new ApiStudent(student);
    }

    [HttpDelete("{studentId}")]
    public async Task Delete(Guid studentId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student with that ID not found!");

        await studentService.Delete(student);
    }

    [HttpGet("{studentId}/institution")]
    public async Task<ApiInstitution> GetStudentInstitution(Guid studentId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var institution = await studentService.GetInstitution(student);

        if (institution is null)
            throw new ThonApiNotFoundException("Student no attached to the institution!");

        return new ApiInstitution(institution);
    }

    [HttpPost("{studentId}/institution/{institutionId}")]
    public async Task<ApiInstitution> AttachStudentToInstitution(Guid studentId, Guid institutionId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var institution = await institutionService.Get(institutionId);
        ThonApiNotFoundException.ThrowIfNull(institution, "Institution not found!");

        var studentInstitution = await studentService.GetInstitution(student);

        if (studentInstitution is not null)
            throw new ThonApiBadRequestException("Student already attached to the institution!");

        await studentService.Attach(student, institution);
        return new ApiInstitution(institution);
    }

    [HttpDelete("{studentId}/institution")]
    public async Task DeattachStudentFromInstitution(Guid studentId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        await studentService.Deattach(student);
    }

    [HttpGet("{studentId}/faculty")]
    public async Task<ApiFaculty> GetStudentFaculty(Guid studentId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var faculty = await studentService.GetFaculty(student);

        if (faculty is null)
            throw new ThonApiNotFoundException("Student no attached to the faculty!");

        return new ApiFaculty(faculty);
    }

    [HttpPost("{studentId}/faculty/{facultyId}")]
    public async Task<ApiFaculty> AttachStudentToFaculty(Guid studentId, Guid facultyId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var studentFaculty = await studentService.GetFaculty(student);

        if (studentFaculty is not null)
            throw new ThonApiBadRequestException("Student already attached to the faculty!");

        var studentInstitution = await studentService.GetInstitution(student);

        if (studentInstitution is null)
            throw new ThonApiBadRequestException("Student not attached to the institution!");

        var faculty = await facultyService.Get(facultyId);
        ThonApiNotFoundException.ThrowIfNull(faculty, "Faculty not found!");
        
        if (studentInstitution.Id != faculty.InstitutionId)
            throw new ThonApiBadRequestException("Invalid faculty for that institution!");

        await studentService.Attach(student, faculty);
        return new ApiFaculty(faculty);
    }

    [HttpGet("{studentId}/group")]
    public async Task<ApiGroup> GetStudentGroup(Guid studentId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var group = await studentService.GetGroup(student);

        if (group is null)
            throw new ThonApiNotFoundException("Student no attached to the group!");

        return new ApiGroup(group);
    }

    [HttpPost("{studentId}/group/{groupId}")]
    public async Task<ApiGroup> AttachStudentToGroup(Guid studentId, Guid groupId)
    {
        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var studentGroup = await studentService.GetGroup(student);

        if (studentGroup is not null)
            throw new ThonApiBadRequestException("Student already attached to the group!");

        var studentFaculty = await studentService.GetFaculty(student);

        if (studentFaculty is null)
            throw new ThonApiBadRequestException("Student not attached to the faculty!");

        var group = await groupService.Get(groupId);
        ThonApiNotFoundException.ThrowIfNull(group, "Faculty not found!");

        if (studentFaculty.Id != group.FacultyId)
            throw new ThonApiBadRequestException("Invalid faculty for that institution!");

        await studentService.Attach(student, group);
        return new ApiGroup(group);
    }
}
