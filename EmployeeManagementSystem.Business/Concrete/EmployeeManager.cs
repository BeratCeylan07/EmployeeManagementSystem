using AutoMapper;
using EmployeeManagementSystem.Business.Abstract;
using EmployeeManagementSystem.Business.Concrete.Dtos.Employees;
using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Concrete;

public class EmployeeManager : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly JwtService _jwtService;
    public EmployeeManager(IEmployeeRepository employeeRepository, IMapper mapper, JwtService jwtService)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _jwtService= jwtService;
    }
    public EmployeeDetailDto GetEmployeeDetails(int employeeId)
    {
        var employee = _employeeRepository.GetEmployeeWithDetails(employeeId);
        if (employee == null)
        {
            return null;
        }

        return _mapper.Map<EmployeeDetailDto>(employee);
    }
    public bool Update(UpdateEmployeeDto updateEmployeeDto)
    {
        if (updateEmployeeDto == null)
        {
            throw new ArgumentNullException(nameof(updateEmployeeDto));
        }

        var existingEmployee = _employeeRepository.GetById(updateEmployeeDto.Id);
        if (existingEmployee == null)
        {
            return false;
        }

        _mapper.Map(updateEmployeeDto, existingEmployee);
    
        // Güncelleme tarihini ve kullanıcısını ayarla
        existingEmployee.ISMODIFIEDDATE = DateTime.Now;
        existingEmployee.ISMODIFIEDUSERID = updateEmployeeDto.userID;

        int result = _employeeRepository.Update(existingEmployee);
        return result > 0;
    }
    public bool Add(AddEmployeeDto employeeDto)
    {
        if (employeeDto == null)
        {
            throw new ArgumentNullException(nameof(employeeDto));
        }

        Employee employee = _mapper.Map<Employee>(employeeDto);
        
        int result = _employeeRepository.Add(employee);
        return result > 0;    
    }

    public List<EmployeeWithDepartmentDto> GetAllEmployees()
    {
        var employees = _employeeRepository.GetAll();
        return _mapper.Map<List<EmployeeWithDepartmentDto>>(employees);
    }
    public bool Delete(int employeeId)
    {
        if (_employeeRepository.HasEmployees(employeeId))
        {
            throw new InvalidOperationException("Cannot delete Employee with existing employees.");
        }

        var employee = _employeeRepository.GetById(employeeId);
        if (employee == null)
        {
            return false;
        }

        int result = _employeeRepository.Delete(employee);
        return result > 0;
    }
    public LoginResultDto Login(LoginRequestDto loginRequest)
    {
        var employee = _employeeRepository.GetByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        if (employee == null)
        {
            return new LoginResultDto { Success = false, Message = "Invalid email or password" };
        }

        var token = _jwtService.GenerateToken(employee.ID, employee.Email);

        return new LoginResultDto
        {
            Success = true,
            EmployeeId = employee.ID,
            Name = employee.Name,
            Surname = employee.Surname,
            Email = employee.Email,
            Token = token
        };
    }
    
}