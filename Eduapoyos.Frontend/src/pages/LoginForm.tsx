import { Tabs, Form, Input, Button, Select, Card, Row, Col, Typography, message } from "antd";
import { useNavigate } from "react-router-dom";

const { TabPane } = Tabs;
const { Title } = Typography;

interface LoginResponse {
  token: string;
  rol: "Estudiante" | "Asesor"; // 👈 coincide con tu enum
}

const AuthPage = () => {
  const navigate = useNavigate();
  const [form] = Form.useForm();

  const login = async (values: any) => {
    const response = await fetch("https://localhost:7185/api/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(values),
    });
    if (!response.ok) throw new Error("Login inválido");
    return await response.json();
  };

  const register = async (values: any) => {
    const response = await fetch("https://localhost:7185/api/auth/register", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(values),
    });
    if (!response.ok) throw new Error("Error en registro");
    return await response.json();
  };

  const onLoginFinish = async (values: any) => {
    try {
      const response = await fetch("https://localhost:7185/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(values),
      });
  
      if (!response.ok) throw new Error("Login inválido");
  
      const result: LoginResponse = await response.json();
  
      localStorage.setItem("token", result.token);
      message.success("Login exitoso");
      form.resetFields();
  
      if (result.rol === "Estudiante") {
        navigate("/estudiante/dashboard");
      } else if (result.rol === "Asesor") {
        navigate("/asesor/dashboard");
      } else {
        navigate("/"); // fallback
      }
    } catch {
      message.error("Credenciales inválidas");
    }
  };

  const onRegisterFinish = async (values: any) => {
    try {
      await register(values);
      message.success("Usuario registrado correctamente");
      form.resetFields();
    } catch {
      message.error("Error al registrar usuario");
    }
  };

  return (
    <Row justify="center" align="middle" style={{ minHeight: "100vh", background: "#f0f2f5" }}>
      <Col xs={22} sm={16} md={10} lg={8}>
        <Card bordered={false} style={{ borderRadius: 12, boxShadow: "0 4px 12px rgba(0,0,0,0.1)" }}>
          <Title level={3} style={{ textAlign: "center", marginBottom: 24 }}>
            Bienvenido
          </Title>
          <Tabs defaultActiveKey="1" centered>
            <TabPane tab="Login" key="1">
              <Form onFinish={onLoginFinish} layout="vertical">
                <Form.Item name="userName" label="Nombre de Usuario" rules={[{ required: true }]}>
                  <Input placeholder="Ingresa tu usuario" />
                </Form.Item>
                <Form.Item name="password" label="Contraseña" rules={[{ required: true }]}>
                  <Input.Password placeholder="Ingresa tu contraseña" />
                </Form.Item>
                <Button type="primary" htmlType="submit" block>
                  Iniciar sesión
                </Button>
              </Form>
            </TabPane>

            <TabPane tab="Registro" key="2">
              <Form form={form} onFinish={onRegisterFinish} layout="vertical">
                <Form.Item name="nombreCompleto" label="Nombre completo" rules={[{ required: true }]}>
                  <Input placeholder="Tu nombre completo" />
                </Form.Item>
                <Form.Item name="email" label="Email" rules={[{ required: true }]}>
                  <Input placeholder="Tu correo electrónico" />
                </Form.Item>
                <Form.Item name="userName" label="Nombre de usuario" rules={[{ required: true }]}>
                    <Input placeholder="Tu nombre de usuario" />
                </Form.Item>
                <Form.Item name="password" label="Contraseña" rules={[{ required: true }]}>
                  <Input.Password placeholder="Crea una contraseña" />
                </Form.Item>
                <Form.Item name="rol" label="Rol" rules={[{ required: true }]}>
                  <Select placeholder="Selecciona tu rol">
                    <Select.Option value={0}>Asesor</Select.Option>
                    <Select.Option value={1}>Estudiante</Select.Option>
                  </Select>
                </Form.Item>
                <Button type="dashed" htmlType="submit" block>
                  Registrarse
                </Button>
              </Form>
            </TabPane>
          </Tabs>
        </Card>
      </Col>
    </Row>
  );
};

export default AuthPage;