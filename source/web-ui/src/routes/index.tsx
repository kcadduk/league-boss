import * as React from "react";
import { createFileRoute } from "@tanstack/react-router";
import LoginForm from "@/components/authentication-01";

export const Route = createFileRoute("/")({
  component: HomeComponent,
});

function HomeComponent() {
  return (
    <div className="grid grid-cols-2 gap-3">
      <div className="">left</div>
      <div className="">right</div>
    </div>
  );
}
