import { Table, Button } from "antd";

const SolicitudesTable = ({ solicitudes }) => {
  const descargarConstancia = async (id: string) => {
    const response = await fetch(`https://localhost:7185/api/solicitudes/${id}/constancia`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
    });
    const blob = await response.blob();
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = `Constancia_${id}.pdf`;
    a.click();
    a.remove();
  };

  const columns = [
    { title: "ID", dataIndex: "id", key: "id" },
    { title: "Estado", dataIndex: "estado", key: "estado" },
    {
      title: "Acciones",
      key: "acciones",
      render: (_, record) => (
        <Button onClick={() => descargarConstancia(record.id)}>Descargar PDF</Button>
      )
    }
  ];

  return <Table dataSource={solicitudes} columns={columns} rowKey="id" />;
};

export default SolicitudesTable;