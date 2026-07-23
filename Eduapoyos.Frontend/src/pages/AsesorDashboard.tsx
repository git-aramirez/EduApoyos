import { useEffect, useState } from "react";
import { Table, Form, Input, Button, Select, Card, message } from "antd";
import { useNavigate } from "react-router-dom";

const { Option } = Select;

interface Estudiante {
  id: string;
  nombreCompleto: string;
  email: string;
}

interface Solicitud {
  id: string;
  tipoApoyo: string;
  montoSolicitado: number;
  descripcion: string;
  estado: string;
  estudianteId: string;
}

const AsesorDashboard = () => {
  const [estudiantes, setEstudiantes] = useState<Estudiante[]>([]);
  const [solicitudes, setSolicitudes] = useState<Solicitud[]>([]);
  const [form] = Form.useForm();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchEstudiantes = async () => {
      const response = await fetch("https://localhost:7185/api/estudiante", {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      if (!response.ok) {
        console.error("Error al obtener estudiantes", response.status);
        return;
      }
      setEstudiantes(await response.json());
    };
    fetchEstudiantes();
  }, []);

  useEffect(() => {
    const fetchSolicitudes = async () => {
      const response = await fetch("https://localhost:7185/api/solicitud", {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      if (!response.ok) {
        console.error("Error al obtener solicitudes", response.status);
        return;
      }
      setSolicitudes(await response.json());
    };
    fetchSolicitudes();
  }, []);

  const onCreateEstudiante = async (values: any) => {
    try {
      const response = await fetch("https://localhost:7185/api/estudiante", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify(values)
      });
      if (!response.ok) throw new Error();
      const nuevo = await response.json();
      setEstudiantes([...estudiantes, nuevo]);
      message.success("Estudiante creado correctamente");
      form.resetFields();
    } catch {
      message.error("Error al crear estudiante");
    }
  };

  const cambiarEstado = async (id: string, nuevoEstado: string) => {
    try {
      const response = await fetch(`https://localhost:7185/api/solicitud/${id}/estado`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify({ estado: nuevoEstado })
      });
      if (!response.ok) throw new Error();
      const actualizada = await response.json();
      setSolicitudes(solicitudes.map(s => s.id === id ? actualizada : s));
      message.success("Estado actualizado");
    } catch {
      message.error("Error al actualizar estado");
    }
  };

  return (
    <div style={{ padding: 24 }}>
      <h2>Dashboard Asesor</h2>

      {/* Tabla estudiantes */}
      <Card title="Estudiantes">
        <Table
          dataSource={estudiantes}
          rowKey="id"
          columns={[
            { title: "Nombre", dataIndex: "nombreCompleto" },
            { title: "Email", dataIndex: "email" }
          ]}
        />
      </Card>

      {/* Formulario crear estudiante */}
      <Card title="Crear Estudiante" style={{ marginTop: 24 }}>
        <Form form={form} layout="vertical" onFinish={onCreateEstudiante}>
          <Form.Item name="nombreCompleto" label="Nombre Completo" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="email" label="Email" rules={[{ required: true, type: "email" }]}>
            <Input />
          </Form.Item>
          <Button type="primary" htmlType="submit">Crear</Button>
        </Form>
      </Card>

      {/* Tabla solicitudes con filtros */}
      <Card title="Solicitudes" style={{ marginTop: 24 }}>
        <Table
          dataSource={solicitudes}
          rowKey="id"
          columns={[
            { title: "Tipo Apoyo", dataIndex: "tipoApoyo", filters: [
              { text: "Beca", value: "Beca" },
              { text: "Subsidio", value: "Subsidio" },
              { text: "Credito", value: "Credito" }
            ], onFilter: (value, record) => record.tipoApoyo === value },
            { title: "Monto", dataIndex: "montoSolicitado" },
            { title: "Estado", dataIndex: "estado", filters: [
              { text: "Pendiente", value: "Pendiente" },
              { text: "Aprobada", value: "Aprobada" },
              { text: "Rechazada", value: "Rechazada" }
            ], onFilter: (value, record) => record.estado === value },
            {
              title: "Acciones",
              render: (_, record) => (
                <>
                  <Button onClick={() => navigate(`/solicitud/${record.id}`)}>Ver detalle</Button>
                  <Select
                    defaultValue={record.estado}
                    style={{ width: 120, marginLeft: 8 }}
                    onChange={(nuevoEstado) => cambiarEstado(record.id, nuevoEstado)}
                  >
                    <Option value="Pendiente">Pendiente</Option>
                    <Option value="Aprobada">Aprobada</Option>
                    <Option value="Rechazada">Rechazada</Option>
                  </Select>
                </>
              )
            }
          ]}
        />
      </Card>
    </div>
  );
};

export default AsesorDashboard;