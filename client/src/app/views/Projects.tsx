import React, { useEffect, useState } from "react";
import Project from "../api/project";


export default function Projects() {
    const [projects, setProjects] = useState<Project[]>([]);

    useEffect(() => {
        populateProjects();
    }, []);

    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                        Add entry
                    </button>
                </div>

                <div className="w-1/2 flex justify-end">
                    <form>
                        <input
                            className="border rounded-full py-2 px-4"
                            type="search"
                            placeholder="Search"
                            aria-label="Search"
                        />
                        <button
                            className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
                            type="submit"
                        >
                            Search
                        </button>
                    </form>
                </div>
            </div>

            <table className="table-fixed w-full">
            <thead className="bg-gray-200">
                <tr>
                    <th className="border px-4 py-2 w-12">#</th>
                    <th className="border px-4 py-2">Project Name</th>
                    <th className="border px-4 py-2">Deadline</th>
                    <th className="border px-4 py-2">Reserved time</th>
                </tr>
            </thead>
            <tbody>
                {projects.map((item, index) => (
                  <tr key={index}>
                    <td className="border px-4 py-2 w-12">{item.id}</td>
                    <td className="border px-4 py-2"><a href="redirect to reservations of time">{item.name}</a></td>
                    <td className="border px-4 py-2">{item.deadLine}</td>
                    <td className="border px-4 py-2">{item.reservationSum} min.</td>
                  </tr>
                ))}
            </tbody>
        </table>
        </>
    );

    async function populateProjects() {
        const BASE_URL = "http://localhost:3001/api";
        const response = await fetch(`${BASE_URL}/projects`);
        const data = await response.json();

        setProjects(data);
    }
}
