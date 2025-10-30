import Login from "./components/Login";
import Projects from "./components/Projects";

function App() {
  const token = localStorage.getItem("token");
  return token ? <Projects /> : <Login />;
}

export default App;
