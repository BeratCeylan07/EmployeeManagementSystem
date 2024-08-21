using AutoMapper;
using EmployeeManagementSystem.Business.Abstract;
using EmployeeManagementSystem.Business.Concrete.Dtos.Departments;
using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Concrete;

public class DepartmentManager : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentManager(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<bool> Add(AddDepartmentDto departmentDto)
    {
        if (departmentDto == null)
        {
            throw new ArgumentNullException(nameof(departmentDto));
        }

        Department department = _mapper.Map<Department>(departmentDto);
        
        int result = await _departmentRepository.Add(department);
        return result > 0;
    }
    public async Task<IEnumerable<DepartmentWithEmployeeCountDto>> GetAllDepartmentsWithEmployeeCount()
    {
        var departments = await _departmentRepository.GetAllWithEmployeeCount();
        return _mapper.Map<List<DepartmentWithEmployeeCountDto>>(departments);
    }
    public async Task<IEnumerable<Department>> GetAllDepartments()
    {
        return await _departmentRepository.GetAllAsync();
    }
    public async Task<DepartmentDetailDto> GetDepartmentWithEmployees(int departmentId)
    {
        var department = await _departmentRepository.GetDepartmentWithEmployees(departmentId);
        if (department == null)
        {
            return null;
        }
        var departmentDetailDto = _mapper.Map<DepartmentDetailDto>(department);
        return departmentDetailDto;
    }
    public async Task<bool> Update(UpdateDepartmentDto updateDepartmentDto)
    {
        if (updateDepartmentDto == null)
        {
            throw new ArgumentNullException(nameof(updateDepartmentDto));
        }

        var existingDepartment = await _departmentRepository.GetById(updateDepartmentDto.ID);
        if (existingDepartment == null)
        {
            return false;
        }

    
        // Güncelleme tarihini ve kullanıcısını ayarla
        _mapper.Map(updateDepartmentDto, existingDepartment);
        existingDepartment.ISMODIFIEDDATE = DateTime.Now;
        existingDepartment.ISMODIFIEDUSERID = updateDepartmentDto.UserId;


        int result = await _departmentRepository.Update(existingDepartment);
        return result > 0;
    }

    public async Task<bool> Delete(int departmentId)
    {
        if (await _departmentRepository.HasEmployees(departmentId))
        {
            throw new InvalidOperationException("Cannot delete department with existing employees.");
        }

        var department = await _departmentRepository.GetById(departmentId);
        if (department == null)
        {
            return false;
        }

        int result = await _departmentRepository.Delete(department);
        return result > 0;
    }
    
}