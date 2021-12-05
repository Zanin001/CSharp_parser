@.str = private unnamed_addr constant [3 * i8] c"teste\00", align 1 
; Function Attrs; noinline nounwind optnone uwtable 
define dso_local i32 @main() #0 { 
%1 = alloca i32, align 4
call i32 (i8*, ...) @printf( i8* %1 ) nounwind
}
declare dso_local i32 @printf(i8*, ...) #1
