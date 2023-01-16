using Microsoft.EntityFrameworkCore;
using Entidades.RecursosHumanos;
using Entidades.Viaticos;
using Entidades.Funciones;
using Entidades;

namespace apicea.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<DeptoUe> DeptosUe { get; set; } //
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Viaticos> Viaticos { get; set;}
        public DbSet<ViaticosCiudad> ViaticosCiudades { get; set; }
        public DbSet<ViaticosEstado> ViaticosEstados { get;set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<ViaticosOfi> ViaticosOficinas { get;set; }
        public DbSet<ViaticosPais> ViaticosPaises { get;set; }
        public DbSet<ViaticosPart> ViaticosPartidas { get; set; }
        public DbSet<ListaViaticosPorEmpleado> listaViaticosPorEmpleados { get;set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CEA");

            modelBuilder.Entity<DeptoUe>(entity =>
            {
                entity.HasKey(e => e.IdCea);
                entity.ToTable("DEPTOS_UE");

                entity.Property(e => e.Id).HasPrecision(3).HasColumnName("ID");
                entity.Property(e => e.IdCea).HasPrecision(3).HasColumnName("ID_CEA");
                entity.Property(e => e.IdShpoa).HasPrecision(3).HasColumnName("ID_SHPOA");
                entity.Property(e => e.Descripcion).HasMaxLength(100).HasColumnName("DESCRIPCION");
                entity.Property(e => e.Nivel).HasPrecision(1).HasColumnName("NIVEL");
                entity.Property(e => e.Oficial).HasPrecision(1).HasColumnName("OFICIAL");
                entity.Property(e => e.IdReporta).HasPrecision(3).HasColumnName("ID_REPORTA");
                entity.Property(e => e.AgrupaPoa).HasPrecision(1).HasColumnName("AGRUPA_POA");
                entity.Property(e => e.Meta).HasPrecision(2).HasColumnName("META");
                entity.Property(e => e.Accion).HasPrecision(2).HasColumnName("ACCION");
                entity.Property(e => e.Prog).HasPrecision(3).HasColumnName("PROG");
                entity.Property(e => e.EmpRespon).HasPrecision(4).HasColumnName("EMP_RESPON");
                entity.Property(e => e.AgrupaDir).HasPrecision(3).HasColumnName("AGRUPDIR");
    });

            modelBuilder.Entity<Empleado>(entity =>
            {

                entity.HasKey(e => e.IdEmpleado);
                entity.ToTable("EMPLEADOS");

                entity.Property(e => e.IdEmpleado).HasPrecision(4).HasColumnName("EMPLEADO");
                entity.Property(e => e.Nombre).HasColumnName("NOMBRE").HasMaxLength(50);
                entity.Property(e => e.Paterno).HasColumnName("PATERNO").HasMaxLength(50);
                entity.Property(e => e.Materno).HasColumnName("MATERNO").HasMaxLength(50);
                entity.Property(e => e.Nivel).HasPrecision(3).HasColumnName("ID_NIV");
                entity.Property(e => e.Depto).HasPrecision(2).HasColumnName("DEPTO");
                entity.Property(e => e.Obra).HasPrecision(6).HasColumnName("OBRA");
                entity.Property(e => e.DeptoPpto).HasPrecision(2).HasColumnName("DEPTOPPTO");
                entity.Property(e => e.Municipio).HasPrecision(1).HasColumnName("MUNICIPIO");
                entity.Property(e => e.Activo).HasMaxLength(2).HasColumnName("ACTIVO");
            });

            modelBuilder.Entity<Viaticos>(entity =>
            {
                entity.HasKey(e => new { e.Oficina, e.Ejercicio, e.NoViat });
                entity.ToTable("VIATICOS");

                entity.Property(e => e.Oficina).HasPrecision(1).HasColumnName("OFICINA");
                entity.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
                entity.Property(e => e.NoViat).HasPrecision(5).HasColumnName("NOVIAT");
                entity.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
                entity.Property(e => e.NoEmp).HasPrecision(4).HasColumnName("NOEMP");
                entity.Property(e => e.OrigenId).HasPrecision(3).HasColumnName("ORIGENID");
                entity.Property(e => e.DestinoId).HasPrecision(3).HasColumnName("DESTINOID");
                entity.Property(e => e.Motivo).HasMaxLength(300).HasColumnName("MOTIVO");
                entity.Property(e => e.FechaSal).HasColumnType("DATE").HasColumnName("FECHASAL");
                entity.Property(e => e.FechaReg).HasColumnType("DATE").HasColumnName("FECHAREG");
                entity.Property(e => e.Dias).HasPrecision(2).HasColumnName("DIAS");
                entity.Property(e => e.InforFecha).HasColumnType("DATE").HasColumnName("INFOR_FECHA");
                entity.Property(e => e.InforAct).HasMaxLength(500).HasColumnName("INFOR_ACT");
                entity.Property(e => e.Nota).HasMaxLength(500).HasColumnName("NOTA");
                entity.Property(e => e.Estatus).HasPrecision(1).HasColumnName("ESTATUS");
                entity.Property(e => e.FechaMod).HasColumnType("DATE").HasColumnName("FECHAMOD");
                entity.Property(e => e.Pol).HasPrecision(4).HasColumnName("POL");
                entity.Property(e => e.PolMes).HasPrecision(2).HasColumnName("POLMES");
                entity.Property(e => e.Caja).HasPrecision(2).HasColumnName("CAJA");
                entity.Property(e => e.CajaVale).HasPrecision(5).HasColumnName("CAJA_VALE");
                entity.Property(e => e.CajaRepo).HasPrecision(5).HasColumnName("CAJA_REPO");
                entity.Property(e => e.NoEmpCea).HasPrecision(4).HasColumnName("NOEMP_CREA");
                entity.Property(e => e.InforResul).HasMaxLength(500).HasColumnName("INFOR_RESUL");

            });

            modelBuilder.Entity<ViaticosCiudad>(entity =>
            {
                entity.HasKey(e => e.IdCiudad);
                entity.ToTable("VIATICOS_CIUDAD");

                entity.Property(e => e.IdCiudad).HasPrecision(3).HasColumnName("IDCIUDAD");
                entity.Property(e => e.IdEstado).HasPrecision(3).HasColumnName("IDESTADO");
                entity.Property(e => e.Ciudad).HasMaxLength(50).HasColumnName("CIUDAD");

            });

            modelBuilder.Entity<ViaticosEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);
                entity.ToTable("VIATICOS_ESTADO");

                entity.Property(e => e.IdEstado).HasPrecision(3).HasColumnName("IDESTADO");
                entity.Property(e => e.IdPais).HasPrecision(3).HasColumnName("IDPAIS");
                entity.Property(e => e.Estado).HasMaxLength(50).HasColumnName("ESTADO");


            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VS_USUARIOS");

                entity.Property(e => e.Activo)
                    .HasPrecision(1)
                    .HasColumnName("ACTIVO");

                //entity.Property(e => e.Activos)
                //    .HasPrecision(1)
                //    .HasColumnName("ACTIVOS");

                //entity.Property(e => e.ActivosNivel)
                //    .HasPrecision(1)
                //    .HasColumnName("ACTIVOS_NIVEL");

                //entity.Property(e => e.Almacen)
                //    .HasPrecision(1)
                //    .HasColumnName("ALMACEN");

                //entity.Property(e => e.AlmacenNivel)
                //    .HasPrecision(1)
                //    .HasColumnName("ALMACEN_NIVEL");

                //entity.Property(e => e.Bd)
                //    .HasPrecision(1)
                //    .HasColumnName("BD");

                //entity.Property(e => e.Caja)
                //    .HasPrecision(1)
                //    .HasColumnName("CAJA");

                //entity.Property(e => e.CajaNivel)
                //    .HasPrecision(1)
                //    .HasColumnName("CAJA_NIVEL");

                //entity.Property(e => e.Compras)
                //    .HasPrecision(1)
                //    .HasColumnName("COMPRAS");

                //entity.Property(e => e.ComprasNivel)
                //    .HasPrecision(1)
                //    .HasColumnName("COMPRAS_NIVEL");

                //entity.Property(e => e.Contabilidad)
                //    .HasPrecision(1)
                //    .HasColumnName("CONTABILIDAD");

                //entity.Property(e => e.ContabilidadNivel)
                //    .HasPrecision(1)
                //    .HasColumnName("CONTABILIDAD_NIVEL");

                entity.Property(e => e.Depto)
                    .HasPrecision(4)
                    .HasColumnName("DEPTO");

                entity.Property(e => e.DeptoDescripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEPTO_DESCRIPCION");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.IdPue)
                    .HasPrecision(3)
                    .HasColumnName("ID_PUE");

                entity.Property(e => e.Login)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.NoEmpleado)
                    .HasPrecision(4)
                    .HasColumnName("NO_EMPLEADO");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(152)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_COMPLETO");

                //entity.Property(e => e.Nominas)
                //    .HasPrecision(1)
                //    .HasColumnName("NOMINAS");

                //entity.Property(e => e.NominasNivel)
                //    .HasPrecision(1)
                //    .HasColumnName("NOMINAS_NIVEL");

                entity.Property(e => e.Pass)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASS");

                //entity.Property(e => e.Polnom)
                //    .HasMaxLength(100)
                //    .IsUnicode(false)
                //    .HasColumnName("POLNOM");

                //entity.Property(e => e.Presupuestos)
                //    .HasPrecision(1)
                //    .HasColumnName("PRESUPUESTOS");

                //entity.Property(e => e.PresupuestosNivel)
                //    .HasPrecision(1)
                 //   .HasColumnName("PRESUPUESTOS_NIVEL");

                entity.Property(e => e.Usuario)
                    .HasPrecision(2)
                    .HasColumnName("USUARIO");

                //entity.Property(e => e.Vales)
                //    .HasPrecision(1)
                //    .HasColumnName("VALES");

                //entity.Property(e => e.ValesNivel)
                //    .HasPrecision(1)
                //    .HasColumnName("VALES_NIVEL");

                entity.Property(e => e.Viaticos)
                    .HasPrecision(1)
                    .HasColumnName("VIATICOS");

                entity.Property(e => e.ViaticosNivel)
                    .HasPrecision(1)
                    .HasColumnName("VIATICOS_NIVEL");

            });

            modelBuilder.Entity<ViaticosOfi>(entity =>
            {
                entity.HasKey(e => e.IdOfi);
                entity.ToTable("VIATICOS_OFI");

                entity.Property(e => e.IdOfi).HasPrecision(1).HasColumnName("IDOFI");
                entity.Property(e => e.Nombre).HasMaxLength(50).HasColumnName("NOMBRE");
                entity.Property(e => e.RutaTrans).HasMaxLength(100).HasColumnName("RUTATRANS");
            });

            modelBuilder.Entity<ViaticosPais>(entity =>
            {
                entity.HasKey(e => e.IdPais);
                entity.ToTable("VIATICOS_PAIS");

                entity.Property(e => e.IdPais).HasPrecision(3).HasColumnName("IDPAIS");
                entity.Property(e => e.Pais).HasMaxLength(50).HasColumnName("PAIS");
            });

            modelBuilder.Entity<ViaticosPart>(entity =>
            {
                entity.HasKey(e => new { e.Oficina, e.Ejercicio, e.NoViat, e.Partida } );
                entity.ToTable("VIATICOS_PART");

                entity.Property(e => e.Oficina).HasPrecision(1).HasColumnName("OFICINA");
                entity.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
                entity.Property(e => e.NoViat).HasPrecision(5).HasColumnName("NOVIAT");
                entity.Property(e => e.Partida).HasPrecision(6).HasColumnName("PARTIDA");
                entity.Property(e => e.Importe).HasColumnName("IMPORTE");
            });

            modelBuilder.Entity<ListaViaticosPorEmpleado>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.Viatico).HasPrecision(5).HasColumnName("VIATICO");
                entity.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
                entity.Property(e => e.Origen).HasColumnType("VARCHAR2").HasColumnName("ORIGEN");
                entity.Property(e => e.Destino).HasColumnType("VARCHAR2").HasColumnName("DESTINO");
                entity.Property(e => e.Movito).HasColumnName("MOTIVO").HasMaxLength(300);
                entity.Property(e => e.Salida).HasColumnType("DATE").HasColumnName("SALIDA");
                entity.Property(e => e.Regreso).HasColumnType("DATE").HasColumnName("REGRESO");
                entity.Property(e => e.Estatus).HasColumnType("VARCHAR2").HasColumnName("ESTATUS");

                entity.ToFunction("F_LISTAVIATICOSXEMP");

            });

        }

    }
}
