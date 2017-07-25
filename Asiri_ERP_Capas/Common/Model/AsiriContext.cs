namespace Common.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AsiriContext : DbContext
    {
        public AsiriContext()
            : base("name=AsiriContext")
        {
        }

        public virtual DbSet<CLIt01_anamnesis> CLIt01_anamnesis { get; set; }
        public virtual DbSet<CLIt02_archivoEstudioCompl> CLIt02_archivoEstudioCompl { get; set; }
        public virtual DbSet<CLlt03_atencion> CLlt03_atencion { get; set; }
        public virtual DbSet<CLlt04_cie10> CLlt04_cie10 { get; set; }
        public virtual DbSet<CLlt05_cita> CLlt05_cita { get; set; }
        public virtual DbSet<CLlt06_citaDtl> CLlt06_citaDtl { get; set; }
        public virtual DbSet<CLlt07_consultorio> CLlt07_consultorio { get; set; }
        public virtual DbSet<CLlt08_diagnostico> CLlt08_diagnostico { get; set; }
        public virtual DbSet<CLlt09_estadoCita> CLlt09_estadoCita { get; set; }
        public virtual DbSet<CLlt10_estudioCompl> CLlt10_estudioCompl { get; set; }
        public virtual DbSet<CLlt11_evolucion> CLlt11_evolucion { get; set; }
        public virtual DbSet<CLlt12_examenFisico> CLlt12_examenFisico { get; set; }
        public virtual DbSet<CLlt13_funcionVital> CLlt13_funcionVital { get; set; }
        public virtual DbSet<CLlt14_seguro> CLlt14_seguro { get; set; }
        public virtual DbSet<CLlt15_seguroPaciente> CLlt15_seguroPaciente { get; set; }
        public virtual DbSet<CLlt16_tratamiento> CLlt16_tratamiento { get; set; }
        public virtual DbSet<CLlt17_tratamientoDtl> CLlt17_tratamientoDtl { get; set; }
        public virtual DbSet<MSTt01_medioDePago> MSTt01_medioDePago { get; set; }
        public virtual DbSet<MSTt02_piso> MSTt02_piso { get; set; }
        public virtual DbSet<MSTt03_redSocial> MSTt03_redSocial { get; set; }
        public virtual DbSet<MSTt04_sucursal> MSTt04_sucursal { get; set; }
        public virtual DbSet<MSTt05_tipoSucursal> MSTt05_tipoSucursal { get; set; }
        public virtual DbSet<PROt01_categoria> PROt01_categoria { get; set; }
        public virtual DbSet<PROt02_producto> PROt02_producto { get; set; }
        public virtual DbSet<PROt03_productoUnidadDeMedida> PROt03_productoUnidadDeMedida { get; set; }
        public virtual DbSet<PROt04_servicio> PROt04_servicio { get; set; }
        public virtual DbSet<PROt05_tipoDeServicio> PROt05_tipoDeServicio { get; set; }
        public virtual DbSet<PROt06_unidadDeMedida> PROt06_unidadDeMedida { get; set; }
        public virtual DbSet<RHUt01_empleado> RHUt01_empleado { get; set; }
        public virtual DbSet<RHUt02_empleadoEspecialidad> RHUt02_empleadoEspecialidad { get; set; }
        public virtual DbSet<RHUt03_empleadoTurno> RHUt03_empleadoTurno { get; set; }
        public virtual DbSet<RHUt04_especialidad> RHUt04_especialidad { get; set; }
        public virtual DbSet<RHUt05_estadoCivil> RHUt05_estadoCivil { get; set; }
        public virtual DbSet<RHUt06_horario> RHUt06_horario { get; set; }
        public virtual DbSet<RHUt07_paciente> RHUt07_paciente { get; set; }
        public virtual DbSet<RHUt08_periodoRemuneracion> RHUt08_periodoRemuneracion { get; set; }
        public virtual DbSet<RHUt09_persona> RHUt09_persona { get; set; }
        public virtual DbSet<RHUt10_personaRedSocial> RHUt10_personaRedSocial { get; set; }
        public virtual DbSet<RHUt11_tipoDeEmpleado> RHUt11_tipoDeEmpleado { get; set; }
        public virtual DbSet<RHUt12_tipoDocIdentidad> RHUt12_tipoDocIdentidad { get; set; }
        public virtual DbSet<RHUt13_tipoEspecialidad> RHUt13_tipoEspecialidad { get; set; }
        public virtual DbSet<RHUt14_turno> RHUt14_turno { get; set; }
        public virtual DbSet<SISt01_parametro> SISt01_parametro { get; set; }
        public virtual DbSet<SNTt01_entidadFinanciera> SNTt01_entidadFinanciera { get; set; }
        public virtual DbSet<SNTt02_impuesto> SNTt02_impuesto { get; set; }
        public virtual DbSet<SNTt03_moneda> SNTt03_moneda { get; set; }
        public virtual DbSet<SNTt04_tipoComprobante> SNTt04_tipoComprobante { get; set; }
        public virtual DbSet<SNTt05_tipoMedioDePago> SNTt05_tipoMedioDePago { get; set; }
        public virtual DbSet<TNSt01_comprobanteEmitido> TNSt01_comprobanteEmitido { get; set; }
        public virtual DbSet<TNSt02_comprobanteEmitidoDtl> TNSt02_comprobanteEmitidoDtl { get; set; }
        public virtual DbSet<TNSt03_cuota> TNSt03_cuota { get; set; }
        public virtual DbSet<TNSt04_estadoComprobante> TNSt04_estadoComprobante { get; set; }
        public virtual DbSet<TNSt05_estadoDeCuenta> TNSt05_estadoDeCuenta { get; set; }
        public virtual DbSet<TNSt06_medioDePagoDtl> TNSt06_medioDePagoDtl { get; set; }
        public virtual DbSet<TNSt07_numeracion> TNSt07_numeracion { get; set; }
        public virtual DbSet<UBIt01_distrito> UBIt01_distrito { get; set; }
        public virtual DbSet<UBIt02_provincia> UBIt02_provincia { get; set; }
        public virtual DbSet<UBIt03_region> UBIt03_region { get; set; }
        public virtual DbSet<UBIt04_via> UBIt04_via { get; set; }
        public virtual DbSet<UBIt05_zona> UBIt05_zona { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CLIt01_anamnesis>()
                .Property(e => e.descAnamnesis)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt03_atencion>()
                .Property(e => e.codAtencion)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt03_atencion>()
                .HasMany(e => e.CLIt01_anamnesis)
                .WithRequired(e => e.CLlt03_atencion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt03_atencion>()
                .HasMany(e => e.CLlt12_examenFisico)
                .WithRequired(e => e.CLlt03_atencion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt03_atencion>()
                .HasMany(e => e.CLlt13_funcionVital)
                .WithRequired(e => e.CLlt03_atencion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt03_atencion>()
                .HasMany(e => e.CLlt10_estudioCompl)
                .WithRequired(e => e.CLlt03_atencion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt03_atencion>()
                .HasMany(e => e.CLlt08_diagnostico)
                .WithRequired(e => e.CLlt03_atencion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt03_atencion>()
                .HasMany(e => e.CLlt11_evolucion)
                .WithRequired(e => e.CLlt03_atencion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt04_cie10>()
                .Property(e => e.codCie10)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt04_cie10>()
                .Property(e => e.descCie10)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt05_cita>()
                .Property(e => e.codCita)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt05_cita>()
                .Property(e => e.horaInicio)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt05_cita>()
                .Property(e => e.duracionEstimada)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt05_cita>()
                .Property(e => e.obsvCita)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt05_cita>()
                .Property(e => e.mtoTotal)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CLlt05_cita>()
                .HasMany(e => e.CLlt06_citaDtl)
                .WithRequired(e => e.CLlt05_cita)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt05_cita>()
                .HasMany(e => e.TNSt05_estadoDeCuenta)
                .WithRequired(e => e.CLlt05_cita)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt05_cita>()
                .HasMany(e => e.TNSt01_comprobanteEmitido)
                .WithRequired(e => e.CLlt05_cita)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt06_citaDtl>()
                .Property(e => e.precio)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CLlt06_citaDtl>()
                .Property(e => e.cantidad)
                .HasPrecision(18, 6);

            modelBuilder.Entity<CLlt06_citaDtl>()
                .HasMany(e => e.TNSt02_comprobanteEmitidoDtl)
                .WithRequired(e => e.CLlt06_citaDtl)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt07_consultorio>()
                .Property(e => e.codConsultorio)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt07_consultorio>()
                .Property(e => e.obsvPiso)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt07_consultorio>()
                .HasMany(e => e.RHUt01_empleado)
                .WithRequired(e => e.CLlt07_consultorio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt08_diagnostico>()
                .Property(e => e.descDiagnostico)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt09_estadoCita>()
                .Property(e => e.codEstadoCita)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt09_estadoCita>()
                .Property(e => e.descEstadoCita)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt09_estadoCita>()
                .HasMany(e => e.CLlt05_cita)
                .WithRequired(e => e.CLlt09_estadoCita)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt10_estudioCompl>()
                .Property(e => e.descEstudioCompl)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt10_estudioCompl>()
                .HasMany(e => e.CLIt02_archivoEstudioCompl)
                .WithRequired(e => e.CLlt10_estudioCompl)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt11_evolucion>()
                .Property(e => e.descEvolucion)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt12_examenFisico>()
                .Property(e => e.descExamenFisico)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.sistole)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.diastole)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.pulsacion)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.ritmoRespiratorio)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.temperatura)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.altura)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.peso)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt13_funcionVital>()
                .Property(e => e.imc)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt14_seguro>()
                .Property(e => e.nombreSeguro)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt14_seguro>()
                .Property(e => e.abrvSeguro)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt14_seguro>()
                .HasMany(e => e.CLlt15_seguroPaciente)
                .WithRequired(e => e.CLlt14_seguro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt16_tratamiento>()
                .Property(e => e.descTratamiento)
                .IsUnicode(false);

            modelBuilder.Entity<CLlt16_tratamiento>()
                .HasMany(e => e.CLlt17_tratamientoDtl)
                .WithRequired(e => e.CLlt16_tratamiento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLlt17_tratamientoDtl>()
                .Property(e => e.indicacionServicio)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt01_medioDePago>()
                .Property(e => e.descMedioDePago)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt01_medioDePago>()
                .Property(e => e.abrvMedioDePago)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt01_medioDePago>()
                .Property(e => e.photoPath)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt01_medioDePago>()
                .HasMany(e => e.TNSt06_medioDePagoDtl)
                .WithRequired(e => e.MSTt01_medioDePago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MSTt01_medioDePago>()
                .HasMany(e => e.RHUt01_empleado)
                .WithRequired(e => e.MSTt01_medioDePago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MSTt02_piso>()
                .Property(e => e.obsvPiso)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt02_piso>()
                .HasMany(e => e.CLlt07_consultorio)
                .WithRequired(e => e.MSTt02_piso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MSTt03_redSocial>()
                .Property(e => e.nombreRedSocial)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt03_redSocial>()
                .Property(e => e.abrvRedSocial)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt03_redSocial>()
                .HasMany(e => e.RHUt10_personaRedSocial)
                .WithRequired(e => e.MSTt03_redSocial)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MSTt04_sucursal>()
                .Property(e => e.descSucursal)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt04_sucursal>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt04_sucursal>()
                .Property(e => e.numTelefonico01)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt04_sucursal>()
                .Property(e => e.numTelefonico02)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt04_sucursal>()
                .Property(e => e.obsvSucursal)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt04_sucursal>()
                .HasMany(e => e.TNSt01_comprobanteEmitido)
                .WithRequired(e => e.MSTt04_sucursal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MSTt05_tipoSucursal>()
                .Property(e => e.descTipoSucursal)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt05_tipoSucursal>()
                .Property(e => e.abrvSurcursal)
                .IsUnicode(false);

            modelBuilder.Entity<MSTt05_tipoSucursal>()
                .HasMany(e => e.MSTt04_sucursal)
                .WithRequired(e => e.MSTt05_tipoSucursal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROt01_categoria>()
                .Property(e => e.nombreCategoria)
                .IsUnicode(false);

            modelBuilder.Entity<PROt01_categoria>()
                .Property(e => e.descCategoria)
                .IsUnicode(false);

            modelBuilder.Entity<PROt01_categoria>()
                .HasMany(e => e.PROt02_producto)
                .WithRequired(e => e.PROt01_categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROt02_producto>()
                .Property(e => e.codProducto)
                .IsUnicode(false);

            modelBuilder.Entity<PROt02_producto>()
                .Property(e => e.codBarra)
                .IsUnicode(false);

            modelBuilder.Entity<PROt02_producto>()
                .Property(e => e.nombreProductoGenerico)
                .IsUnicode(false);

            modelBuilder.Entity<PROt02_producto>()
                .Property(e => e.nombreProductoComercial)
                .IsUnicode(false);

            modelBuilder.Entity<PROt02_producto>()
                .Property(e => e.descProducto)
                .IsUnicode(false);

            modelBuilder.Entity<PROt02_producto>()
                .Property(e => e.obsvProducto)
                .IsUnicode(false);

            modelBuilder.Entity<PROt02_producto>()
                .HasMany(e => e.PROt03_productoUnidadDeMedida)
                .WithRequired(e => e.PROt02_producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROt03_productoUnidadDeMedida>()
                .Property(e => e.precio)
                .HasPrecision(18, 6);

            modelBuilder.Entity<PROt03_productoUnidadDeMedida>()
                .Property(e => e.precioMinimo)
                .HasPrecision(18, 6);

            modelBuilder.Entity<PROt03_productoUnidadDeMedida>()
                .Property(e => e.precioMaximo)
                .HasPrecision(18, 6);

            modelBuilder.Entity<PROt04_servicio>()
                .Property(e => e.codServicio)
                .IsUnicode(false);

            modelBuilder.Entity<PROt04_servicio>()
                .Property(e => e.nombreServicio)
                .IsUnicode(false);

            modelBuilder.Entity<PROt04_servicio>()
                .Property(e => e.descServicio)
                .IsUnicode(false);

            modelBuilder.Entity<PROt04_servicio>()
                .Property(e => e.precio)
                .HasPrecision(18, 6);

            modelBuilder.Entity<PROt04_servicio>()
                .Property(e => e.precioMinimo)
                .HasPrecision(18, 6);

            modelBuilder.Entity<PROt04_servicio>()
                .Property(e => e.precioMaximo)
                .HasPrecision(18, 6);

            modelBuilder.Entity<PROt04_servicio>()
                .HasMany(e => e.CLlt06_citaDtl)
                .WithRequired(e => e.PROt04_servicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROt05_tipoDeServicio>()
                .Property(e => e.descTipoDeServicio)
                .IsUnicode(false);

            modelBuilder.Entity<PROt05_tipoDeServicio>()
                .HasMany(e => e.PROt04_servicio)
                .WithRequired(e => e.PROt05_tipoDeServicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROt06_unidadDeMedida>()
                .Property(e => e.codUnidadDeMedida)
                .IsUnicode(false);

            modelBuilder.Entity<PROt06_unidadDeMedida>()
                .Property(e => e.descUnidadDeMedida)
                .IsUnicode(false);

            modelBuilder.Entity<PROt06_unidadDeMedida>()
                .HasMany(e => e.PROt03_productoUnidadDeMedida)
                .WithRequired(e => e.PROt06_unidadDeMedida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt01_empleado>()
                .Property(e => e.codEmpleado)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt01_empleado>()
                .Property(e => e.horasPorPeriodo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RHUt01_empleado>()
                .Property(e => e.mtoRemuneracion)
                .HasPrecision(12, 6);

            modelBuilder.Entity<RHUt01_empleado>()
                .HasMany(e => e.CLlt05_cita)
                .WithRequired(e => e.RHUt01_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt01_empleado>()
                .HasMany(e => e.RHUt02_empleadoEspecialidad)
                .WithRequired(e => e.RHUt01_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt01_empleado>()
                .HasMany(e => e.RHUt03_empleadoTurno)
                .WithRequired(e => e.RHUt01_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt01_empleado>()
                .HasMany(e => e.RHUt06_horario)
                .WithRequired(e => e.RHUt01_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt04_especialidad>()
                .Property(e => e.nombreEspecialidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt04_especialidad>()
                .Property(e => e.abrvEspecialidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt04_especialidad>()
                .HasMany(e => e.PROt04_servicio)
                .WithRequired(e => e.RHUt04_especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt04_especialidad>()
                .HasMany(e => e.RHUt02_empleadoEspecialidad)
                .WithRequired(e => e.RHUt04_especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt05_estadoCivil>()
                .Property(e => e.descEstadoCivil)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt05_estadoCivil>()
                .Property(e => e.abrvEstadoCivil)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaInicioLunes)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaFinLunes)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaInicioMartes)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaFinMartes)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaInicioMiercoles)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaFinMiercoles)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaInicioJueves)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaFinJueves)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaInicioViernes)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaFinViernes)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaInicioSabado)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaFinSabado)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaInicioDomingo)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt06_horario>()
                .Property(e => e.horaFinDomingo)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt07_paciente>()
                .Property(e => e.codPaciente)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt07_paciente>()
                .Property(e => e.numHistoriaClinica)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt07_paciente>()
                .Property(e => e.grupoSanguineo)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt07_paciente>()
                .HasMany(e => e.CLlt05_cita)
                .WithRequired(e => e.RHUt07_paciente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt07_paciente>()
                .HasMany(e => e.CLlt15_seguroPaciente)
                .WithRequired(e => e.RHUt07_paciente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt08_periodoRemuneracion>()
                .Property(e => e.codPeriodoRemuneracion)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt08_periodoRemuneracion>()
                .Property(e => e.descPeriodoRemuneracion)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt08_periodoRemuneracion>()
                .Property(e => e.abrvPeriodoRemuneracion)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt08_periodoRemuneracion>()
                .HasMany(e => e.RHUt01_empleado)
                .WithRequired(e => e.RHUt08_periodoRemuneracion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.tipoPersoneria)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.nombreRepresentante)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.nombrePersona)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.apellidoPaterno)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.apellidoMaterno)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.numDocIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.razonSocial)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.nombreVia)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.numVia)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.nombreZona)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.direccion01)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.direccion02)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.numTelefonico01)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.numTelefonico02)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.email01)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.email02)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.sexo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .Property(e => e.obsvPersona)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt09_persona>()
                .HasMany(e => e.RHUt10_personaRedSocial)
                .WithRequired(e => e.RHUt09_persona)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt10_personaRedSocial>()
                .Property(e => e.nombrePerfil)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt10_personaRedSocial>()
                .Property(e => e.obsvPerfil)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt11_tipoDeEmpleado>()
                .Property(e => e.descTipoDeEmpleado)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt11_tipoDeEmpleado>()
                .HasMany(e => e.RHUt01_empleado)
                .WithRequired(e => e.RHUt11_tipoDeEmpleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt12_tipoDocIdentidad>()
                .Property(e => e.codTipoDocIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt12_tipoDocIdentidad>()
                .Property(e => e.descTipoDocIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt12_tipoDocIdentidad>()
                .Property(e => e.abrvTipoDocIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt12_tipoDocIdentidad>()
                .HasMany(e => e.RHUt09_persona)
                .WithRequired(e => e.RHUt12_tipoDocIdentidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt13_tipoEspecialidad>()
                .Property(e => e.nombreEspecialidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt13_tipoEspecialidad>()
                .Property(e => e.descEspecialidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt13_tipoEspecialidad>()
                .Property(e => e.abrvTipoEspecialidad)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt13_tipoEspecialidad>()
                .HasMany(e => e.RHUt04_especialidad)
                .WithRequired(e => e.RHUt13_tipoEspecialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHUt14_turno>()
                .Property(e => e.descTurno)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt14_turno>()
                .Property(e => e.abrvTurno)
                .IsUnicode(false);

            modelBuilder.Entity<RHUt14_turno>()
                .HasMany(e => e.RHUt03_empleadoTurno)
                .WithRequired(e => e.RHUt14_turno)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SISt01_parametro>()
                .Property(e => e.codParametro)
                .IsUnicode(false);

            modelBuilder.Entity<SISt01_parametro>()
                .Property(e => e.nombreParametro)
                .IsUnicode(false);

            modelBuilder.Entity<SISt01_parametro>()
                .Property(e => e.descParametro)
                .IsUnicode(false);

            modelBuilder.Entity<SISt01_parametro>()
                .Property(e => e.valorDeTexto)
                .IsUnicode(false);

            modelBuilder.Entity<SISt01_parametro>()
                .Property(e => e.valorNumerico)
                .HasPrecision(18, 6);

            modelBuilder.Entity<SISt01_parametro>()
                .Property(e => e.valorTextoDefault)
                .IsUnicode(false);

            modelBuilder.Entity<SISt01_parametro>()
                .Property(e => e.valorNumericoDefault)
                .HasPrecision(18, 6);

            modelBuilder.Entity<SNTt01_entidadFinanciera>()
                .Property(e => e.nombreEntidadFinanciera)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt01_entidadFinanciera>()
                .Property(e => e.abrvEntidadFinanciera)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt01_entidadFinanciera>()
                .HasMany(e => e.RHUt01_empleado)
                .WithRequired(e => e.SNTt01_entidadFinanciera)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SNTt02_impuesto>()
                .Property(e => e.codImpto)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt02_impuesto>()
                .Property(e => e.descImpto)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt02_impuesto>()
                .Property(e => e.abrvImpto)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt02_impuesto>()
                .Property(e => e.porcentajeImpto)
                .HasPrecision(10, 6);

            modelBuilder.Entity<SNTt02_impuesto>()
                .HasMany(e => e.TNSt01_comprobanteEmitido)
                .WithRequired(e => e.SNTt02_impuesto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .Property(e => e.codMoneda)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .Property(e => e.codIsoMoneda)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .Property(e => e.codIsoNumMoneda)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .Property(e => e.descMoneda)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .Property(e => e.abrvMoneda)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .Property(e => e.simbolo)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .Property(e => e.fraccion)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .HasMany(e => e.PROt02_producto)
                .WithRequired(e => e.SNTt03_moneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .HasMany(e => e.TNSt06_medioDePagoDtl)
                .WithRequired(e => e.SNTt03_moneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SNTt03_moneda>()
                .HasMany(e => e.TNSt01_comprobanteEmitido)
                .WithRequired(e => e.SNTt03_moneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SNTt04_tipoComprobante>()
                .Property(e => e.codTipoComprobante)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt04_tipoComprobante>()
                .Property(e => e.descTipoComprobante)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt04_tipoComprobante>()
                .HasMany(e => e.TNSt01_comprobanteEmitido)
                .WithRequired(e => e.SNTt04_tipoComprobante)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SNTt05_tipoMedioDePago>()
                .Property(e => e.codTipoMedioDePago)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt05_tipoMedioDePago>()
                .Property(e => e.descTipoMedioDePago)
                .IsUnicode(false);

            modelBuilder.Entity<SNTt05_tipoMedioDePago>()
                .HasMany(e => e.MSTt01_medioDePago)
                .WithRequired(e => e.SNTt05_tipoMedioDePago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.serie)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.numComprobanteEmitido)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.razonAnulacion)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.mtoTotal)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.mtoSubTotal)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.mtoImpto)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.mtoDescuento)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.porcentajeImpto)
                .HasPrecision(10, 6);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.porcentajeDescuento)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.obsvComprobanteEmitido)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.info01)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.info02)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .Property(e => e.info03)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .HasMany(e => e.TNSt02_comprobanteEmitidoDtl)
                .WithRequired(e => e.TNSt01_comprobanteEmitido)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TNSt01_comprobanteEmitido>()
                .HasMany(e => e.TNSt06_medioDePagoDtl)
                .WithRequired(e => e.TNSt01_comprobanteEmitido)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TNSt02_comprobanteEmitidoDtl>()
                .Property(e => e.precio)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt02_comprobanteEmitidoDtl>()
                .Property(e => e.cantidad)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt03_cuota>()
                .Property(e => e.mtoPago)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt03_cuota>()
                .Property(e => e.mtoInteres)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt04_estadoComprobante>()
                .Property(e => e.nombreEstadoComprobante)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt04_estadoComprobante>()
                .Property(e => e.descEstadoComprobante)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.porcentajeDescuento)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.mtoDescuento)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.mtoSubtotal)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.debe)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.haber)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.saldo)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.inicial)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.porcentajeInteres)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .Property(e => e.mtoInteres)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt05_estadoDeCuenta>()
                .HasMany(e => e.TNSt03_cuota)
                .WithRequired(e => e.TNSt05_estadoDeCuenta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TNSt06_medioDePagoDtl>()
                .Property(e => e.mtoMedioDePago)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt06_medioDePagoDtl>()
                .Property(e => e.tipoDeCambio)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TNSt07_numeracion>()
                .Property(e => e.serie)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt07_numeracion>()
                .Property(e => e.num_corr_inicial)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt07_numeracion>()
                .Property(e => e.num_corr_final)
                .IsUnicode(false);

            modelBuilder.Entity<TNSt07_numeracion>()
                .Property(e => e.num_corr_actual)
                .IsUnicode(false);

            modelBuilder.Entity<UBIt01_distrito>()
                .Property(e => e.nombreDistrito)
                .IsUnicode(false);

            modelBuilder.Entity<UBIt02_provincia>()
                .Property(e => e.nombreProvincia)
                .IsUnicode(false);

            modelBuilder.Entity<UBIt02_provincia>()
                .HasMany(e => e.UBIt01_distrito)
                .WithRequired(e => e.UBIt02_provincia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UBIt03_region>()
                .Property(e => e.nombreRegion)
                .IsUnicode(false);

            modelBuilder.Entity<UBIt03_region>()
                .HasMany(e => e.UBIt02_provincia)
                .WithRequired(e => e.UBIt03_region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UBIt04_via>()
                .Property(e => e.descVia)
                .IsUnicode(false);

            modelBuilder.Entity<UBIt04_via>()
                .Property(e => e.abrvVia)
                .IsUnicode(false);

            modelBuilder.Entity<UBIt05_zona>()
                .Property(e => e.descZona)
                .IsUnicode(false);

            modelBuilder.Entity<UBIt05_zona>()
                .Property(e => e.abrvZona)
                .IsUnicode(false);
        }
    }
}
