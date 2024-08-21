using AutoMapper;
using EmployeeManagementSystem.Business.Abstract;
using EmployeeManagementSystem.Business.Concrete.Dtos.EmployeePayments;
using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;

namespace EmployeeManagementSystem.Business.Concrete;

public class EmployeePaymentManager : IEmployeePaymentService
{
    private readonly IEmployeePaymentRepository _employeePaymentRepository;
    private readonly IMapper _mapper;

    public EmployeePaymentManager(IEmployeePaymentRepository employeePaymentRepository, IMapper mapper)
    {
        _employeePaymentRepository = employeePaymentRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> AddAsync(CreateEmployeePaymentDto addEmployeePaymentDto)
    {
        if (addEmployeePaymentDto is null)
        {
            throw new ArgumentNullException(nameof(addEmployeePaymentDto));
        }
        EmployeePayment employeePayment = _mapper.Map<EmployeePayment>(addEmployeePaymentDto);
        
        int result = await _employeePaymentRepository.Add(employeePayment);
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdateEmployeePaymentDto updateEmployeePaymentDto)
    {
        if (updateEmployeePaymentDto is null)
        {
            throw new ArgumentNullException(nameof(updateEmployeePaymentDto));
        }
        
        var existingEmplooyePayment = await _employeePaymentRepository.GetById(updateEmployeePaymentDto.Id);
        if (existingEmplooyePayment is null)
        {
            throw new ArgumentNullException(nameof(updateEmployeePaymentDto));
        }
        
        _mapper.Map(updateEmployeePaymentDto, existingEmplooyePayment);
        
        existingEmplooyePayment.ISMODIFIEDDATE = DateTime.Now;
        existingEmplooyePayment.ISMODIFIEDUSERID = updateEmployeePaymentDto.UserId;
        
        int result = await _employeePaymentRepository.Update(existingEmplooyePayment);
        return result > 0;

    }

    public async Task<bool> DeleteAsync(int emplooyePaymentId)
    {
        if (await _employeePaymentRepository.HasEmployeesAsync(emplooyePaymentId))
        {
            throw new InvalidOperationException("Cannot delete Emplooye Payment with existing employees.");
        }
        
        var emplooyePayment = await _employeePaymentRepository.GetById(emplooyePaymentId);
        if (emplooyePayment is null)
        {
            return false;
        }
        
        int result = await _employeePaymentRepository.Delete(emplooyePayment);

        return result > 0;
    }

    public async Task<List<EmployeePaymentDto>> GetAllPaymentsAsync()
    {
        var employeePayments = await _employeePaymentRepository.GetAllPaymentsAsync();
        return _mapper.Map<List<EmployeePaymentDto>>(employeePayments);
    }
    public async Task<EmployeePaymentDetailDto> GetEmplooyeePaymentInfoAsync(int departmentId)
    {
        var emplooyePayment = await _employeePaymentRepository.GetById(departmentId);
        if (emplooyePayment is null)
        {
            return null;
        }
        
        return _mapper.Map<EmployeePaymentDetailDto>(emplooyePayment);
    }
}