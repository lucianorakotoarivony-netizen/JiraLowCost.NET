export interface AuthResponse{
    id: string,
    token: string,
    username: string,
    role: string
}

export interface TaskItem{
    id: number,
    title: string,
    description: string,
    status: string,
    priority: string,
    difficulty: string,
    createdBy: string,
    assignedTo: string | null,
    createdAt: string,
}

export interface CreateTaskItem{
    title: string,
    description: string,
    status: string,
    priority: string,
    assignedToId: string,
}

export interface UserResponse{
    id: string,
    username: string,
    role: string,
}