import { Routes, Route } from "react-router-dom";
import LoginForm from "./pages/LoginForm";
import EstudianteDashboard from "./pages/EstudianteDashboard";

function App() {
  return (
    <Routes>
      <Route path="/" element={<LoginForm />} />
      <Route path="/estudiante/dashboard" element={<EstudianteDashboard />} />
    </Routes>
  );
}

export default App;