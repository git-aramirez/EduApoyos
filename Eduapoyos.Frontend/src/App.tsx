import { Routes, Route } from "react-router-dom";
import LoginForm from "./pages/LoginForm";
import EstudianteDashboard from "./pages/EstudianteDashboard";
import AsesorDashboard from "./pages/AsesorDashboard";
import DetalleSolicitud from "./pages/DetalleSolicitud"; // detalle de solicitud

function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginForm />} />
      <Route path="/estudiante/dashboard" element={<EstudianteDashboard />} />
      <Route path="/asesor/dashboard" element={<AsesorDashboard />} />
      <Route path="/solicitudes/:id" element={<DetalleSolicitud />} />
    </Routes>
  );
}

export default App;