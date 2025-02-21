import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Todo } from '../../models/todo.model';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  selector: 'app-todo',
  imports: [CommonModule, MatIconModule, MatCheckboxModule],
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent implements OnInit {
  todos: Todo[] = [];

  private baseUrl = 'http://localhost:5241/ToDo/get'
  private addUrl = 'http://localhost:5241/ToDo/add'
  private deleteUrl = 'http://localhost:5241/ToDo/delete'
  private toggleUrl = 'http://localhost:5241/ToDo/toggle'

  constructor(private http: HttpClient) {
    console.log('Initial todos:', this.todos);
  }

  ngOnInit() {
    this.getTodos();
  }

  getTodos() {
    this.http.get<Todo[]>(this.baseUrl).subscribe({
      next: (data) => {
        this.todos = data;
        console.log('Fetched todos:', this.todos);
      },
      error: (error) => console.error('Error fetching todos:', error)
    });
  }

  addTodo(item: string) {
    if (!item.trim()) return;
    
    const newTodo = { item, completed: false };

    this.http.post<Todo>(this.addUrl, newTodo).subscribe({
      next: (todo) => this.todos.push(todo),  
      error: (error) => console.error('Error adding todo:', error) 
    });
    
  }
  
  toggleTodo(id: number) {
    const url = `${this.toggleUrl}/${id}`;
    this.http.put(url, {id}).subscribe({
      next: () => {
        this.getTodos();
      },
      error: (error) => {
        console.error('Error toggling todo:', error);
        this.getTodos();
      }
    });
  }

  deleteTodo(id: number) {
    this.todos = this.todos.filter(todo => todo.id !== id);
    const url = `${this.deleteUrl}/${id}`;
    
    this.http.delete(url).subscribe({
      next: () => {
        this.getTodos();
      },
      error: (error) => {
        console.error('Error deleting todo:', error);
        this.getTodos();
      }
    });
  }
}

