import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Card, Descriptions, Select, message } from "antd";

const { Option } = Select;

interface Solicitud {
  id: string;
  tipoApoyo: string;
  montoSolicitado: number;
  descripcion: string;
  estado: string;
  estudianteId: string;
  fechaSolicitud: string;
  fechaActualizacion: string;
}

const DetalleSolicitud = () => {
  const { id } = useParams(); // 👈 obtiene el id de la URL
  const [solicitud, setSolicitud] = useState<Solicitud | null>(null);

  useEffect(() => {
    const fetchDetalle = async () => {
      const response = await fetch(`https://localhost:7185/api/solicitudes/${id}`, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      if (!response.ok) {
        message.error("Error al obtener detalle de la solicitud");
        return;
      }
      setSolicitud(await response.json());
    };
    fetchDetalle();
  }, [id]);

  const cambiarEstado = async (nuevoEstado: string) => {
    try {
      const response = await fetch(`https://localhost:7185/api/solicitudes/${id}/estado`, {
        method: "PATCH", // 👈 tu backend usa PATCH para cambiar estado
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify({ estado: nuevoEstado })
      });
      if (!response.ok) throw new Error();
      const actualizada = await response.json();
      setSolicitud(actualizada);
      message.success("Estado actualizado correctamente");
    } catch {
      message.error("Error al actualizar estado");
    }
  };

  if (!solicitud) return <p>Cargando...</p>;

  return (
    <Card title={`Detalle Solicitud #${solicitud.id}`} style={{ margin: 24 }}>
      <Descriptions bordered column={1}>
        <Descriptions.Item label="Tipo de Apoyo">{solicitud.tipoApoyo}</Descriptions.Item>
        <Descriptions.Item label="Monto">{solicitud.montoSolicitado}</Descriptions.Item>
        <Descriptions.Item label="Descripción">{solicitud.descripcion}</Descriptions.Item>
        <Descriptions.Item label="Estado">
          <Select
            defaultValue={solicitud.estado}
            style={{ width: 200 }}
            onChange={cambiarEstado}
          >
            <Option value="Pendiente">Pendiente</Option>
            <Option value="Aprobada">Aprobada</Option>
            <Option value="Rechazada">Rechazada</Option>
          </Select>
        </Descriptions.Item>
        <Descriptions.Item label="EstudianteId">{solicitud.estudianteId}</Descriptions.Item>
        <Descriptions.Item label="Fecha Solicitud">{solicitud.fechaSolicitud}</Descriptions.Item>
        <Descriptions.Item label="Última Actualización">{solicitud.fechaActualizacion}</Descriptions.Item>
      </Descriptions>
    </Card>
  );
};

export default DetalleSolicitud;