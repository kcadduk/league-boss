import * as React from "react";
import { createFileRoute } from "@tanstack/react-router";
import LoginForm from "@/components/authentication-01";

export const Route = createFileRoute("/")({
  component: HomeComponent,
});

function HomeComponent() {
  return (
    <div className="grid grid-cols-2 md:cols-1">
      <div className="hidden md:block h-screen overflow-hidden border-r-2 border-black">
        <img
          src="https://images.unsplash.com/photo-1544281153-6603be88b354?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
          className="h-full w-auto min-w-full object-cover"
        />
      </div>
      <div className="flex md:flex-grow bg-green-100 items-center place-content-center">
        <div className="shadow-2xl">
          <LoginForm />
        </div>
      </div>
    </div>
  );
}
