import { useEffect, useState } from "react";
import { Form, Input, InputNumber, Button, Select, message, Card } from "antd";
import SolicitudesTable from "../components/SolicitudesTable";

const { Option } = Select;

interface SolicitudApoyoResponseDto {
    id: string;
    tipoApoyo: string;
    monto: number;
    descripcion: string;
  }

const EstudianteDashboard = () => {
 const [solicitudes, setSolicitudes] = useState<SolicitudApoyoResponseDto[]>([]);

  // 🔹 Cargar solicitudes existentes
  useEffect(() => {
    const fetchSolicitudes = async () => {
      const response = await fetch("https://localhost:7185/api/estudiantes/solicitudes", {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });

      if (!response.ok) {
        console.error("Error al obtener solicitudes");
        return;
      }

      const data = await response.json();
      setSolicitudes(data);
    };

    fetchSolicitudes();
  }, []);

  // 🔹 Crear nueva solicitud
  const onFinish = async (values: any) => {
    try {
      const solicitud = {
        estudianteId: "GUID_DEL_ESTUDIANTE",   // 👈 debes obtenerlo del contexto o token
        asesorId: "GUID_DEL_ASESOR",           // 👈 idem
        tipoApoyo: values.tipoApoyo,           // enum esperado por el backend
        montoSolicitado: values.monto,         // ojo: el nombre debe ser MontoSolicitado
        descripcion: values.descripcion,
        estado: "Pendiente",                   // puedes fijar un valor inicial
        fechaSolicitud: new Date().toISOString(),
        fechaActualizacion: new Date().toISOString()
      };
  
      const response = await fetch("https://localhost:7185/api/solicitudes", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify(solicitud)
      });
  
      if (!response.ok) throw new Error("Error al crear solicitud");
  
      const nueva = await response.json();
      setSolicitudes([...solicitudes, nueva]);
      message.success("Solicitud creada correctamente");
    } catch {
      message.error("No se pudo crear la solicitud");
    }
  };

  return (
    <div style={{ padding: 24 }}>
      <h2>Mis Solicitudes</h2>
      <SolicitudesTable solicitudes={solicitudes} />

      <Card style={{ marginTop: 24 }}>
        <h3>Crear nueva solicitud</h3>
        <Form layout="vertical" onFinish={onFinish}>
        {/* EstudianteId */}
        <Form.Item name="estudianteId" label="Estudiante" rules={[{ required: true }]}>
            <Select placeholder="Selecciona un estudiante">
            {/* 👇 Aquí luego llenas con opciones reales */}
            <Option value="00000000-0000-0000-0000-000000000000">Estudiante 1</Option>
            <Option value="11111111-1111-1111-1111-111111111111">Estudiante 2</Option>
            </Select>
        </Form.Item>

        {/* AsesorId */}
        <Form.Item name="asesorId" label="Asesor" rules={[{ required: true }]}>
            <Select placeholder="Selecciona un asesor">
            {/* 👇 Aquí luego llenas con opciones reales */}
            <Option value="22222222-2222-2222-2222-222222222222">Asesor 1</Option>
            <Option value="33333333-3333-3333-3333-333333333333">Asesor 2</Option>
            </Select>
        </Form.Item>

        {/* TipoApoyo */}
        <Form.Item name="tipoApoyo" label="Tipo de Apoyo" rules={[{ required: true }]}>
            <Select placeholder="Selecciona el tipo de apoyo">
            <Option value="Beca">Beca</Option>
            <Option value="Subsidio">Subsidio</Option>
            <Option value="Credito">Crédito</Option>
            </Select>
        </Form.Item>

        {/* MontoSolicitado */}
        <Form.Item name="montoSolicitado" label="Monto solicitado" rules={[{ required: true }]}>
            <InputNumber min={100} style={{ width: "100%" }} />
        </Form.Item>

        {/* Descripcion */}
        <Form.Item name="descripcion" label="Descripción" rules={[{ required: true }]}>
            <Input.TextArea rows={3} placeholder="Describe tu solicitud" />
        </Form.Item>

        {/* Estado */}
        <Form.Item name="estado" label="Estado" rules={[{ required: true }]}>
            <Select placeholder="Selecciona el estado inicial">
            <Option value="Pendiente">Pendiente</Option>
            <Option value="Aprobada">Aprobada</Option>
            <Option value="Rechazada">Rechazada</Option>
            </Select>
        </Form.Item>

        <Button type="primary" htmlType="submit" block>
            Crear Solicitud
        </Button>
        </Form>
      </Card>
    </div>
  );
};

export default EstudianteDashboard;