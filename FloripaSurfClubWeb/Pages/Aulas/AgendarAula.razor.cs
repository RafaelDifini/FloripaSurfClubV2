using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Requests.Aulas;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubWeb.Handlers;
using FloripaSurfClubWeb.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Claims;

namespace FloripaSurfClubWeb.Pages.Aulas
{
    public class AgendarAulaPage : ComponentBase
    {
        #region Proriedades
        public DateTime? HorarioSelecionado { get; set; } = null;
        public List<DateTime> HorariosDisponiveis { get; set; } = new();
        public bool IsBusy { get; set; }
        public CreateAulaRequest InputModel { get; set; } = new();
        public List<Professor> Professores { get; set; } = new List<Professor>();
        public string AlunoNome { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject]
        public IAulasHandler AulasHandler { get; set; } = null!;
        [Inject]
        public IAlunoHandler AlunosHandler { get; set; } = null!;
        [Inject]
        public IProfessorHandler ProfessoresHandler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/login");
                return;
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var alunoResponse = await AlunosHandler.GetByIdAsync(new GetAlunoByIdRequest { UsuarioSistemaId = Guid.Parse(userId) });
                if (alunoResponse.IsSuccess)
                {
                    AlunoNome = alunoResponse.Data?.Nome ?? string.Empty;
                    InputModel.AlunosId.Add(alunoResponse.Data.Id);
                }
                else
                {
                    Snackbar.Add("Erro ao carregar as informações do aluno.", Severity.Error);
                }
            }

            var professoresResponse = await ProfessoresHandler.GetAllAsync(new GetAllProfessorsRequest());
            if (professoresResponse.IsSuccess && professoresResponse.Data.Any())
            {
                Professores = professoresResponse.Data;
                InputModel.ProfessorId = Professores.First().Id;
                await CarregarHorariosDisponiveisAsync();
            }
            else
            {
                Snackbar.Add("Erro ao carregar a lista de professores.", Severity.Error);
            }

            StateHasChanged();
        }

        #endregion

        #region Metodos
        protected async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await AulasHandler.AgendarAulaAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add("Aula agendada com sucesso.", Severity.Success);
                    //await CarregarHorariosDisponiveisAsync();
                }
                else
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task CarregarHorariosDisponiveisAsync()
        {
            if (InputModel.ProfessorId != Guid.Empty && InputModel.DataInicio.HasValue)
            {
                var request = new ObterHorariosDisponiveisRequest
                {
                    ProfessorId = InputModel.ProfessorId,
                    DataSelecionada = InputModel.DataInicio.Value
                };

                var response = await AulasHandler.ObterHorariosDisponiveisAsync(request);
                if (response.IsSuccess)
                {
                    HorariosDisponiveis = response.Data;

                    HorarioSelecionado = HorariosDisponiveis.FirstOrDefault();
                }
                else
                {
                    HorarioSelecionado = null;
                    Snackbar.Add(response.Message, Severity.Error);
                }

                StateHasChanged();
            }
        }

        public async Task OnProfessorChanged(Guid professorId)
        {
            InputModel.ProfessorId = professorId;
            HorarioSelecionado = null; 
            await CarregarHorariosDisponiveisAsync();
        }

        public async Task OnDateChanged(DateTime? selectedDate)
        {
            InputModel.DataInicio = selectedDate;
            HorarioSelecionado = null;  
            await CarregarHorariosDisponiveisAsync();
        }

        public void OnHorarioChanged(DateTime horario)
        {
            InputModel.DataInicio = horario;
            HorarioSelecionado = horario;
        }



        #endregion
    }
}
