using KinectService;
using KinectService.Movimento;
using KinectService.Movimento.Pose;
using KinectService.Sensor;
using KinectService.Sensor.Ativadores;
using KinectService.SensorCor;
using KinectService.SensorCor.Ativadores;
using KinectService.SensorEsqueleto;
using KinectService.SensorEsqueleto.Ativadores;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCC2_PadraoCorpo_v2.CRUD;
using TCC2_PadraoCorpo_v2.CRUD.Entidade;


namespace TCC2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Tarefa tarefa;
        TipoTarefa tipoTarefa;
        Captura captura;
        Usuario usuario;

        ISensorService sensorService;
        ICorService corService;
        IEsqueletoService esqueletoService;
        IRastreador rastrearMovimento;
        ApresentacaoMain apresentacao;
        bool PoseIdentificada = false;
        bool gravarCaptura = false;
        int idTarefa = 0;

        public MainWindow()
        {
            this.WindowState = System.Windows.WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;

            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            cbTipoTarefas.ItemsSource = TipoTarefa.TarefasAtivas().Select(w => w.nome);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.sensorService = new SensorService<AtivadorSimples>();
            this.sensorService.ConfigurarSensor(KinectSensor.KinectSensors, ColorImageFormat.RgbResolution640x480Fps30, true);

            if (this.sensorService.statusSensor == enumStatusSensor.Ativo)
            {
                this.apresentacao = new ApresentacaoMain(cvParaDesenhar, this.sensorService.kinectSensor);

                this.rastrearMovimento = new Rastreador<PoseSentado>();
                this.corService = new CorService<CoresRGB30fps>(this.sensorService.kinectSensor);
                this.esqueletoService = new EsqueletoService<EsqueletoSimples>(this.sensorService.kinectSensor);

                this.sensorService.kinectSensor.AllFramesReady += kinectSensor_AllFramesReady;
                this.sensorService.kinectSensor.Start();
            }
        }

        private void kinectSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            this.corService.GerarImagem(cvParaDesenhar, e);
            this.esqueletoService.DefinirIntervaloCaptura(2.5F, 1.0F);
            this.esqueletoService.CapturarPrimeiroEsqueletoCena(e);

            if (this.esqueletoService.personagens.Count > 0)
            {
                var personagem = this.esqueletoService.personagens.FirstOrDefault();
                this.rastrearMovimento.Rastrear(personagem, out PoseIdentificada);
                if (this.PoseIdentificada)
                {
                    txtAnguloCabeca.Text = string.Format("Cabeça: {0}° | ", ((int)this.rastrearMovimento.anguloCabeca));
                    txtAnguloColuna.Text = string.Format("Coluna: {0}° | ", ((int)this.rastrearMovimento.anguloColuna));
                    txtAnguloJoelho.Text = string.Format("Joelho: {0}°", ((int)this.rastrearMovimento.anguloJoelho));

                    if (gravarCaptura)
                    {
                        this.captura = new Captura()
                        {
                            capturaCabeca = this.rastrearMovimento.anguloCabeca,
                            capturaColuna = this.rastrearMovimento.anguloColuna,
                            capturaJoelho = this.rastrearMovimento.anguloJoelho,
                            idTarefa = this.idTarefa
                        };
                        IValorCaptura gravarAngulosCaptura = new InserirValorCaptura<Captura>();
                        gravarAngulosCaptura.inserirValorCapturado(captura);
                    }



                    this.DesenharComponenteVirtual(personagem.Joints[JointType.Head], ComponenteInternoCabeca, ComponenteExternoCabeca, (double)this.rastrearMovimento.anguloCabeca);
                    this.DesenharComponenteVirtual(personagem.Joints[JointType.Spine], ComponenteInternoColuna, ComponenteExternoColuna, (double)this.rastrearMovimento.anguloColuna);
                    this.DesenharComponenteVirtual(personagem.Joints[JointType.KneeRight], ComponenteInternoJoelho, ComponenteExternoJoelho, (double)this.rastrearMovimento.anguloJoelho);
                }
            }
        }

        private void DesenharComponenteVirtual(Joint articulacao, FrameworkElement elementA, FrameworkElement elementB, double porcentagem = 0)
        {
            CircularControlAngle circular = (CircularControlAngle)elementB;
            circular.Percentage = this.rastrearMovimento.anguloCabeca;
            this.apresentacao.DesenharComponenteVirtual(articulacao, elementA, elementB, porcentagem);
        }

        private void btnIniciarAtividade_Click(object sender, RoutedEventArgs e)
        {
            if (this.usuario.idUsuario > 0 && !string.IsNullOrEmpty((String)cbTipoTarefas.SelectedValue))
            {
                this.tarefa = new Tarefa()
                {
                    idTipoTarefa = TipoTarefa.IdTarefa((String)this.cbTipoTarefas.SelectedValue),
                    idUsuario = this.usuario.idUsuario,
                    inicioTarefa = DateTime.Now,
                    fimtarefa = DateTime.Now
                };

                IValorCaptura gravarTarefaCaptura = new InserirValorCaptura<Tarefa>();
                this.idTarefa = gravarTarefaCaptura.inserirValorCapturado(this.tarefa);
                if (this.idTarefa > 0)
                {
                    this.gravarCaptura = true;
                }
            }
        }

        private void btnPararAtividade_Click(object sender, RoutedEventArgs e)
        {
            this.gravarCaptura = false;
            this.idTarefa = 0;
            this.tarefa.atualizarTerminoTarefa(DateTime.Now);
        }

        private void btnNovoUsuario_Click(object sender, RoutedEventArgs e)
        {
            this.tarefa = null;
            this.tipoTarefa = null;
            this.captura = null;
            this.usuario = null;
            this.gravarCaptura = false;
            this.idTarefa = 0;
            ComboBoxItem typeItem = (ComboBoxItem)this.cbSexo.SelectedItem;
            this.usuario = new Usuario()
            {
                idade = Convert.ToInt32(this.txtidade.Text),
                sexo = (string)typeItem.Content
            };
            IValorCaptura gravarUsuarioCaptura = new InserirValorCaptura<Usuario>();
            this.usuario.idUsuario = gravarUsuarioCaptura.inserirValorCapturado(this.usuario);
        }
    }
}
