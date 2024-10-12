import * as React from "react";
import {createFileRoute} from "@tanstack/react-router";

export const Route = createFileRoute("/")({
    beforeLoad: ({context: {queryClient}}) => {
        queryClient.ensureQueryData({
            queryKey: ['hello-world'],
            queryFn: async () => {
                const response = await fetch('/api/hello-world');
                return response;
            }
        })
    },
    component: HomeComponent,
});

function HomeComponent() {
    return (
        <div className="p-2">
            <h1 className="text-3xl font-bold">Welcome Home!</h1>
        </div>
    );
}
