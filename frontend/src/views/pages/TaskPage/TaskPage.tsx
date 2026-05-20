import { useState, useEffect } from "react";
import { getAllTasks } from "../../../external-api-clients/clients/externalTaskApiClient";
import { getAllBooks } from "../../../external-api-clients/clients/externalBookApiClient";
import type { Task } from "../../../entities/Task";
import type { Book } from "../../../entities/Book";
import TaskTable from "./TaskTable/TaskTable";
import TaskModal from "./TaskModal/TaskModal";

export default function TasksPage() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedTask, setSelectedTask] = useState<Task | null>(null);

  useEffect(() => {
    Promise.all([getAllTasks(), getAllBooks()])
      .then(([tasks, books]) => {
        setTasks(tasks);
        setBooks(books);
      })
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  const selectedBookTitle = selectedTask
    ? (() => {
        console.log("selectedTask.bookId:", selectedTask.bookId);
        console.log("books:", books);
        return books.find(
          (b) => b.id?.toString() === selectedTask.bookId?.toString(),
        )?.title;
      })()
    : undefined;

  if (loading) return <p>Loading tasks...</p>;
  if (error) return <p>Error: {error}</p>;

  return (
    <>
      <TaskTable tasks={tasks} onStart={(task) => setSelectedTask(task)} />
      <TaskModal
        task={selectedTask}
        bookTitle={selectedBookTitle}
        onClose={() => setSelectedTask(null)}
      />
    </>
  );
}
