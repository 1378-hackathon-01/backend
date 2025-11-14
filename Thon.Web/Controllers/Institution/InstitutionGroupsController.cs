using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thon.App.Services;
using Thon.Core.Models;
using Thon.Web.Authorization;
using Thon.Web.Entities.Institution;
using Thon.Web.Exceptions;

namespace Thon.Web.Controllers.Institution;

[ApiController]
[Authorize(Policy = AuthPolicies.Institution)]
[Route("faculties")]
public class InstitutionGroupsController(
    UserInstitutionService userInstitutionService,
    UserInstitutionAuthService userInstitutionAuthService,
    InstitutionService institutionService,
    FacultyService facultyService,
    StudentService studentService,
    GroupService groupService)
    : BaseInstitutionController(
        userInstitutionService,
        userInstitutionAuthService,
        institutionService)
{
    [HttpGet("{facultyId}/groups/{groupId}")]
    public async Task<InstitutionGroupBrief> Get(Guid facultyId, Guid groupId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        return new InstitutionGroupBrief(group);
    }

    [HttpGet("{facultyId}/groups")]
    public async Task<IReadOnlyList<InstitutionGroupBrief>> Get(Guid facultyId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        var groups = await groupService.Get(faculty);

        return groups
            .Select(x => new InstitutionGroupBrief(x))
            .ToList();
    }

    [HttpPost("{facultyId}/groups")]
    public async Task Create(Guid facultyId, [FromBody] InstitutionGroupPost request)
    {
        ThonApiBadRequestException.ThrowIfNull(request);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Title);
        ThonApiBadRequestException.ThrowIfNullOrWhiteSpace(request.Abbreviation);

        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        await groupService.Create(
            faculty: faculty,
            title: request.Title.Trim(),
            abbreviation: request.Abbreviation.Trim());
    }

    [HttpDelete("{facultyId}/groups/{groupId}")]
    public async Task Delete(Guid facultyId, Guid groupId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        await groupService.Delete(group);
    }

    [HttpDelete("{facultyId}/groups/{groupId}/students/approved/{studentId}")]
    public async Task DeleteApprovedStudent(
        Guid facultyId, 
        Guid groupId,
        Guid studentId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var approve = await studentService.ApproveGet(student);
        ThonApiNotFoundException.ThrowIfNull(approve, "Approve not found!");

        if (approve.InstitutionId != institution.Id
            || approve.FacultyId != faculty.Id
            || approve.GroupId != group.Id)
            throw new ThonApiNotFoundException("Group does not contain student approve!");

        await studentService.ApproveDelete(approve);
    }

    [HttpGet("{facultyId}/groups/{groupId}/students/requests")]
    public async Task<IReadOnlyList<InstitutionStudentRequestFull>> GetStudentRequests(
        Guid facultyId,
        Guid groupId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        var requestChains = await groupService
            .GetRequests(
                group: group);

        var responses = new List<InstitutionStudentRequestFull>();

        foreach (var requestChain in requestChains)
        {
            var student = await studentService.Get(requestChain.Institution.StudentId);
            ThonApiNotFoundException.ThrowIfNull(student);

            var response = new InstitutionStudentRequestFull(student);
            responses.Add(response);
        }

        return responses;
    }

    [HttpPost("{facultyId}/groups/{groupId}/students/requests/{studentId}")]
    public async Task ApproveStudentRequest(
        Guid facultyId,
        Guid groupId,
        Guid studentId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var requestChain = await studentService.GetRequest(student);
        ThonApiNotFoundException.ThrowIfNull(requestChain, "Student request not found!");

        if (requestChain.Institution.InstitutionId != institution.Id
            || requestChain.Faculty.FacultyId != faculty.Id
            || requestChain.Group.GroupId != group.Id)
            throw new ThonApiNotFoundException("Group does not contain student request!");

        await studentService
            .RequestApprove(
                student: student, 
                requestChain:  requestChain);
    }

    [HttpDelete("{facultyId}/groups/{groupId}/students/requests/{studentId}")]
    public async Task DeclineStudentRequest(
        Guid facultyId,
        Guid groupId,
        Guid studentId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        var student = await studentService.Get(studentId);
        ThonApiNotFoundException.ThrowIfNull(student, "Student not found!");

        var requestChain = await studentService.GetRequest(student);
        ThonApiNotFoundException.ThrowIfNull(requestChain, "Student request not found!");

        if (requestChain.Institution.InstitutionId != institution.Id
            || requestChain.Faculty.FacultyId != faculty.Id
            || requestChain.Group.GroupId != group.Id)
            throw new ThonApiNotFoundException("Group does not contain student request!");

        await studentService
            .RequestDecline(
                student: student,
                requestChain: requestChain);
    }

    [HttpGet("{facultyId}/groups/{groupId}/students/approved")]
    public async Task<IReadOnlyList<InstitutionStudentApproveFull>> GetStudentsApproved(
        Guid facultyId,
        Guid groupId)
    {
        var institution = await GetInstitution();
        var faculty = await facultyService.Get(facultyId);
        var group = await groupService.Get(groupId);

        if (faculty is null || faculty.InstitutionId != institution.Id)
            throw new ThonApiNotFoundException("Faculty not found!");

        if (group is null || group.FacultyId != faculty.Id)
            throw new ThonApiNotFoundException("Group not found!");

        var approves = await groupService.GetApproves(group);
        var students = new List<Student>();

        foreach (var approve in approves)
        {
            var student = await studentService.Get(approve.StudentId);
            ThonApiNotFoundException.ThrowIfNull(student, "Student approve not found!");

            students.Add(student);
        }

        return students
            .Select(x => new InstitutionStudentApproveFull(x))
            .ToList();

    }
}
