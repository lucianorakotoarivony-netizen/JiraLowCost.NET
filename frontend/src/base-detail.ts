import { inject, computed, OnInit, Signal, Injectable, DestroyRef, signal } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Data } from "./app/Services/data";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
@Injectable()
export abstract class BaseDetail<T extends {id:T["id"]}> implements OnInit{
    public dataService = inject(Data);
    public route = inject(ActivatedRoute);
    public router = inject(Router);
    errorStatus = signal<boolean>(false);
    errorMessage = signal<string | null>(null);
    private destroyRef = inject(DestroyRef);
    abstract detailData : Signal<T | null>;
    abstract listData : Signal<T[]>;
    abstract backRoute : string;
    abstract loadDetail(id:string):void;
    abstract loadList():void;
    ngOnInit(): void {
        this.loadList();
        this.route.paramMap.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(params => {
            const id = params.get("id");
            if (id) this.loadDetail(id);
        });
    }
    currentIndex = computed(() => {
        const list = this.listData();
        const currentId = this.detailData()?.id;
        return list.findIndex(item => item.id === currentId);
    })
    prev = computed(() => {
        const list = this.listData();
        const index = this.currentIndex();
        if (list.length === 0 || list.length === -1) return null;
        const prevIndex = (index - 1 + list.length) % list.length;
        return list[prevIndex];
    });
    next = computed(() => {
        const list = this.listData();
        const index = this.currentIndex();
        if (list.length === 0 || list.length === -1) return null;
        const nextIndex = (index + 1) % list.length;
        return list[nextIndex];
    });
    goBack(){
        this.router.navigate([this.backRoute]);
    }
    retryLoad(){
        this.loadList();
        const id = this.route.snapshot.paramMap.get("id");
        if (id) this.loadDetail(id);
    }
}