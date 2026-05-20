import { useState, useEffect } from "react";
import { getAllTasks } from "../../../external-api-clients/clients/externalTaskApiClient";
import type { Task } from "../../../entities/Task";
import TaskTable from "./TaskTable/TaskTable";
import TaskModal from "./TaskModal/TaskModal";

export default function TasksPage() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedTask, setSelectedTask] = useState<Task | null>(null);

  useEffect(() => {
    getAllTasks()
      .then(setTasks)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Loading tasks...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <>
      <TaskTable tasks={tasks} onStart={(task) => setSelectedTask(task)} />
      <TaskModal task={selectedTask} onClose={() => setSelectedTask(null)} />
    </>
  );
}
