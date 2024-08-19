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

    public bool Add(AddDepartmentDto departmentDto)
    {
        if (departmentDto == null)
        {
            throw new ArgumentNullException(nameof(departmentDto));
        }

        Department department = _mapper.Map<Department>(departmentDto);
        
        int result = _departmentRepository.Add(department);
        return result > 0;
    }
    public List<DepartmentWithEmployeeCountDto> GetAllDepartmentsWithEmployeeCount()
    {
        var departments = _departmentRepository.GetAllWithEmployeeCount();
        return _mapper.Map<List<DepartmentWithEmployeeCountDto>>(departments);
    }
    public List<Department> GetAllDepartments()
    {
        return _departmentRepository.GetAll();
    }
    public DepartmentDetailDto GetDepartmentWithEmployees(int departmentId)
    {
        var department = _departmentRepository.GetDepartmentWithEmployees(departmentId);
        if (department == null)
        {
            return null;
        }
        var departmentDetailDto = _mapper.Map<DepartmentDetailDto>(department);
        return departmentDetailDto;
    }
    public bool Update(UpdateDepartmentDto updateDepartmentDto)
    {
        if (updateDepartmentDto == null)
        {
            throw new ArgumentNullException(nameof(updateDepartmentDto));
        }

        var existingDepartment = _departmentRepository.GetById(updateDepartmentDto.ID);
        if (existingDepartment == null)
        {
            return false;
        }

        _mapper.Map(updateDepartmentDto, existingDepartment);
    
        // Güncelleme tarihini ve kullanıcısını ayarla
        existingDepartment.ISMODIFIEDDATE = DateTime.Now;
        existingDepartment.ISMODIFIEDUSERID = updateDepartmentDto.UserId;

        int result = _departmentRepository.Update(existingDepartment);
        return result > 0;
    }

    public bool Delete(int departmentId)
    {
        if (_departmentRepository.HasEmployees(departmentId))
        {
            throw new InvalidOperationException("Cannot delete department with existing employees.");
        }

        var department = _departmentRepository.GetById(departmentId);
        if (department == null)
        {
            return false;
        }

        int result = _departmentRepository.Delete(department);
        return result > 0;
    }
    
}