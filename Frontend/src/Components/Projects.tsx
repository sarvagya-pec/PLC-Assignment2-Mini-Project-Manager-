import { useEffect, useState } from "react";
import API from "../api";

interface Project {
  id: number;
  name: string;
  description: string;
}

export default function Projects() {
  const [projects, setProjects] = useState<Project[]>([]);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");

  useEffect(() => {
    fetchProjects();
  }, []);

  const fetchProjects = async () => {
    const res = await API.get("/projects");
    setProjects(res.data);
  };

  const addProject = async () => {
    await API.post("/projects", { name, description });
    setName("");
    setDescription("");
    fetchProjects();
  };

  return (
    <div style={{ padding: 20 }}>
      <h2>Your Projects</h2>
      <input placeholder="Name" value={name} onChange={(e) => setName(e.target.value)} />
      <input placeholder="Description" value={description} onChange={(e) => setDescription(e.target.value)} />
      <button onClick={addProject}>Add Project</button>
      <ul>
        {projects.map((p) => (
          <li key={p.id}>{p.name} – {p.description}</li>
        ))}
      </ul>
    </div>
  );
}
